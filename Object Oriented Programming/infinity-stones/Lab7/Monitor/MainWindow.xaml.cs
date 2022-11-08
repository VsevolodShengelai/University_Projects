using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MeasuringDevice;

namespace Monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private MeasureMassDevice device;

        private EventHandler newMeasurementTaken;

        private void startCollecting_Click(object sender, RoutedEventArgs e)
        {
            if (device is null)
                device = new MeasureMassDevice(Units.Metric, "LogFile.txt");

            newMeasurementTaken = device_NewMeasurementTaken;

            device.NewMeasurementTaken += newMeasurementTaken;
 
            loggingFileNameBox.Text = device.GetLoggingFile();
            unitsBox.Text = device.UnitsToUse.ToString();
        }

        private void device_NewMeasurementTaken(object sender, System.EventArgs e)
        {
            if (device is null) return;
            mostRecentMeasureBox.Text = device.MostRecentMeasure.ToString();
            metricValueBox.Text = device.MetricValue().ToString();
            imperialValueBox.Text = device.ImperialValue().ToString();
            rawDataValues.Items.Clear();
            foreach (int measure in device.GetRawData())
            {
                rawDataValues.Items.Add(measure);
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (device is null)
            {
                MessageBox.Show("You must create an instance of the MeasureMassDevice class first.");
                return;
            }
            device.LoggingFileName = loggingFileNameBox.Text;
        }

        private void dispose_Click(object sender, RoutedEventArgs e)
        {
            if (device is null) return;
            
            device.Dispose();
            device = null;
        }

        private void stopCollecting_Click(object sender, RoutedEventArgs e)
        {
            if (device is null) return;
            device.StopCollecting();

            device.NewMeasurementTaken -= device_NewMeasurementTaken;
        }
    }
}
