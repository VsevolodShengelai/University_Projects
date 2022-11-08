using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DeviceControl;

namespace MeasuringDevice
{
    public class MeasureLengthDevice : MeasureDataDevice
    {
        /// <summary>
        /// Construct a new instance of the MeasureLengthDevice class.
        /// </summary>
        /// <param name="deviceUnits">Specifies the units used by the device.</param>
        public MeasureLengthDevice(Units deviceUnits)
        {
            unitsToUse = deviceUnits;
        }

        /// <summary>
        /// Converts the raw data collected by the measuring device into a metric value.
        /// </summary>
        /// <returns>The latest measurement from the device converted to metric units.</returns>
        public override decimal MetricValue()
        {
            decimal metricMostRecentMeasure;

            if (unitsToUse == Units.Metric)
            {
                metricMostRecentMeasure = Convert.ToDecimal(mostRecentMeasure);
            }
            else
            {
                // Imperial measurements are in inches.
                // Multiply imperial measurement by 25.4 to convert from inches to millimeters. 
                // Convert from an integer value to a decimal.
                decimal decimalImperialValue = Convert.ToDecimal(mostRecentMeasure);
                decimal conversionFactor = 25.4M;
                metricMostRecentMeasure = decimalImperialValue * conversionFactor;
            }

            return metricMostRecentMeasure;
        }

        /// <summary>
        /// Converts the raw data collected by the measuring device into an imperial value.
        /// </summary>
        /// <returns>The latest measurement from the devcie converted to imperial units.</returns>
        public override decimal ImperialValue()
        {
            decimal imperialMostRecentMeasure;

            if (unitsToUse == Units.Imperial)
            {
                imperialMostRecentMeasure = Convert.ToDecimal(mostRecentMeasure);
            }
            else
            {
                // Metric measurements are in millimeters.
                // Multiply metric measurement by 25.4 to convert from millimeters to inches. 
                // Convert from an integer value to a decimal.
                decimal decimalMetricValue = Convert.ToDecimal(mostRecentMeasure);
                decimal conversionFactor = 0.3937M;
                imperialMostRecentMeasure = decimalMetricValue * conversionFactor;
            }

            return imperialMostRecentMeasure;
        }
    }
}
