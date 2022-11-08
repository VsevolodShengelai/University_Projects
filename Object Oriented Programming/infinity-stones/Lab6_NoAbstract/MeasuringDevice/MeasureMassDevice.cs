using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MeasuringDevice
{
    public class MeasureMassDevice : IMeasuringDevice
    {
        Units unitsToUse;
        int[] dataCaptured;
        int mostRecentMeasurement;
        DeviceControl.DeviceController controller;
        const DeviceControl.DeviceType measurementType = DeviceControl.DeviceType.MASS;

        public MeasureMassDevice(Units unitsToUse)
        {
            this.unitsToUse = unitsToUse;
        }

        public int[] GetRawData()
        {
            return dataCaptured;
        }

        public decimal ImperialValue()
        {
            if (unitsToUse == Units.Imperial)
            {
                return mostRecentMeasurement;
            }
            return Convert.ToDecimal(mostRecentMeasurement * 2.2046);
        }

        public decimal MetricValue()
        {
            if (unitsToUse == Units.Metric)
            {
                return mostRecentMeasurement;
            }
            return Convert.ToDecimal(mostRecentMeasurement * 0.4536);
        }

        public void StopCollecting()
        {
            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }
        }

        public void StartCollecting()
        {
            controller = DeviceControl.DeviceController.StartDevice(measurementType);
            GetMeasurements();
        }
        private void GetMeasurements()
        {
            dataCaptured = new int[10];
            System.Threading.ThreadPool.QueueUserWorkItem((dummy) =>
            {
                int x = 0;
                Random timer = new Random();

                while (controller != null)
                {
                    System.Threading.Thread.Sleep(timer.Next(1000, 5000));
                    dataCaptured[x] = controller != null ? controller.TakeMeasurement() : dataCaptured[x];
                    mostRecentMeasurement = dataCaptured[x];

                    x++;
                    if (x == 10)
                    {
                        x = 0;
                    }
                }
            });
        }
    }
}
