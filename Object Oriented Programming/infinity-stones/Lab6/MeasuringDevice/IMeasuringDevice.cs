using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuringDevice
{
    public interface IMeasuringDevice
    {
        /// <summary>
        /// Converts the raw data collected by the measuring device into a metric value.
        /// </summary>
        /// <returns>The latest measurement from the device converted to metric units.</returns>
        decimal MetricValue();

        /// <summary>
        /// Converts the raw data collected by the measuring device into an imperial value.
        /// </summary>
        /// <returns>The latest measurement fromt he devcie converted to imperial units.</returns>
        decimal ImperialValue();

        /// <summary>
        /// Starts the measuring device.
        /// </summary>
        void StartCollecting();

        /// <summary>
        /// Stops the measuring device. 
        /// </summary>
        void StopCollecting();

        /// <summary>
        /// Enables access to the raw data from the device in whatever units are native to the device.
        /// </summary>
        /// <returns>The raw data from the device in native format.</returns>
        int[] GetRawData();
    }
}
