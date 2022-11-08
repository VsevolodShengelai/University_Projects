using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using DeviceControl;
using System.IO;
using System.ComponentModel;

namespace MeasuringDevice
{
    public abstract class MeasureDataDevice : IEventEnabledMeasuringDevice, IDisposable
    {
        /// <summary>
        /// Converts the raw data collected by the measuring device into a metric value.
        /// </summary>
        /// <returns>The latest measurement from the device converted to metric units.</returns>
        public abstract decimal MetricValue();

        /// <summary>
        /// Converts the raw data collected by the measuring device into an imperial value.
        /// </summary>
        /// <returns>The latest measurement from the device converted to imperial units.</returns>
        public abstract decimal ImperialValue();

        // BackgroundWorker member to generate measurements.
        private BackgroundWorker dataCollector;

        /// <summary>
        /// Starts the measuring device.
        /// </summary>
        public void StartCollecting()
        {
            if (disposed == true) return;

            if (controller == null)
                controller = DeviceController.StartDevice(measurementType);

            // New code to check the logging file is not already open.
            // If it is already open then write a log message.
            // If not open the logging file.
            if (loggingFileWriter == null)
            {
                // Check if the logging file exists - if not create it.
                if (!File.Exists(loggingFileName))
                {
                    loggingFileWriter = File.CreateText(loggingFileName);
                    loggingFileWriter.WriteLine("Log file status checked - Created");
                    loggingFileWriter.WriteLine("Collecting Started");
                }
                else
                {
                    loggingFileWriter = new StreamWriter(loggingFileName);
                    loggingFileWriter.WriteLine("Log file status checked - Opened");
                    loggingFileWriter.WriteLine("Collecting Started");
                }
            }
            else
            {
                loggingFileWriter.WriteLine("Log file status checked - Already open");
                loggingFileWriter.WriteLine("Collecting Started");
            }

            GetMeasurements();
        }

        // Add a GetMeasurements method to configure and start the 
        // BackgroundWorker.
        private void GetMeasurements()
        {
            dataCollector = new BackgroundWorker();
            dataCollector.WorkerSupportsCancellation = true;
            dataCollector.WorkerReportsProgress = true;
            dataCollector.DoWork += DataCollector_DoWork;
            dataCollector.ProgressChanged += DataCollector_ProgressChanged;
            dataCollector.RunWorkerAsync();
        }

        private void DataCollector_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnNewMeasurementTaken();
        }

        private void DataCollector_DoWork(object sender, DoWorkEventArgs e)
        {
            dataCaptured = new int[10];
            int i = 0;
            while (!dataCollector.CancellationPending)
            {
                Thread.Sleep(1000);
                mostRecentMeasure = controller.TakeMeasurement();
                dataCaptured[i] = mostRecentMeasure;

                loggingFileWriter?.WriteLine($"Measurement - {mostRecentMeasure}");
                dataCollector.ReportProgress(0);
                i++;
                if (i >= 10) i = 0;
            }
        }

        /// <summary>
        /// Stops the measuring device. 
        /// </summary>
        public void StopCollecting()
        {
            if (disposed == true) return;

            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }

            // New code to write to the log.
            if (loggingFileWriter != null)
            {
                loggingFileWriter.WriteLine("Collecting Stopped");
            }

            // Stop the data collection BackgroundWorker.
            dataCollector?.CancelAsync();
        }

        /// <summary>
        /// Enables access to the raw data from the device in whatever units are native to the device.
        /// </summary>
        /// <returns>The raw data from the device in native format.</returns>
        public int[] GetRawData()
        {
            return dataCaptured;
        }

        protected Units unitsToUse;
        protected int[] dataCaptured;
        protected int mostRecentMeasure;
        protected DeviceController controller;
        protected DeviceType measurementType;

        protected string loggingFileName;
        private TextWriter loggingFileWriter;

        /// <summary>
        /// Returns the file name of the logging file for the device.
        /// </summary>
        /// <returns>The file name of the logging file.</returns>
        public string GetLoggingFile()
        {
            return loggingFileName;
        }

        // Flag indicating whether Dispose has been called.
        private bool disposed = false;

        /// <summary>
        /// Dispose method required for the IDispose interface.
        /// </summary>
        public void Dispose()
        {
            // Dispose has been called.
            disposed = true;

            // Check that the log file is closed, if it is not closed log a message and close it.
            if (loggingFileWriter != null)
            {
                loggingFileWriter.WriteLine("Object Disposed");
                loggingFileWriter.Flush();
                loggingFileWriter.Close();
            }

            // Dispose of the dataCollector BackgroundWorker object.
            dataCollector?.Dispose();
        }

        // New properties to provide controlled access to data.

        /// <summary>
        /// Gets the Units used natively by the device.
        /// </summary>
        public Units UnitsToUse
        {
            get
            {
                return unitsToUse;
            }
        }

        /// <summary>
        /// Gets an array of the measurements taken by the device.
        /// </summary>
        public int[] DataCaptured
        {
            get
            {
                return dataCaptured;
            }
        }

        /// <summary>
        /// Gets the most recent measurement taken by the device.
        /// </summary>
        public int MostRecentMeasure
        {
            get
            {
                return mostRecentMeasure;
            }
        }

        /// <summary>
        /// Gets or sets the name of the logging file used. 
        /// If the logging file changes this closes the current file and creates the new file.
        /// </summary>
        public string LoggingFileName
        {
            get
            {
                return loggingFileName;
            }
            set
            {
                if (loggingFileWriter == null)
                {
                    // If the file has not been opened simply update the file name.
                    loggingFileName = value;
                }
                else
                {
                    // If the file has been opened close the current file first,
                    // then update the file name and open the new file.
                    loggingFileWriter.WriteLine("Log File Changed");
                    loggingFileWriter.WriteLine("New Log File: {0}", value);
                    loggingFileWriter.Close();

                    // Now update the logging file and open the new file.
                    loggingFileName = value;

                    // Check if the logging file exists - if not create it.
                    if (!File.Exists(loggingFileName))
                    {
                        loggingFileWriter = File.CreateText(loggingFileName);
                        loggingFileWriter.WriteLine("Log file status checked - Created");
                        loggingFileWriter.WriteLine("Collecting Started");
                    }
                    else
                    {
                        loggingFileWriter = new StreamWriter(loggingFileName);
                        loggingFileWriter.WriteLine("Log file status checked - Opened");
                        loggingFileWriter.WriteLine("Collecting Started");
                    }
                    loggingFileWriter.WriteLine("Log File Changed Successfully");
                }
            }
        }

        public int HeartBeatInterval { get; }
        public event EventHandler HeartBeat;

        // Class implementation of the NewMeasurementTaken event.
        public event EventHandler NewMeasurementTaken;

        // Method to raise the NewMeasurementTaken event.
        protected virtual void OnNewMeasurementTaken()
        {
            if (NewMeasurementTaken.GetInvocationList().Length != 0)
                NewMeasurementTaken(this, null);
        }

    }
}
