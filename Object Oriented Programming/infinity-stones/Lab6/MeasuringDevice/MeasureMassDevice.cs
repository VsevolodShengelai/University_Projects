using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeviceControl;


namespace MeasuringDevice
{
    public class MeasureMassDevice : MeasureDataDevice
    {
        public MeasureMassDevice(Units unitsToUse)
        {
            this.unitsToUse = unitsToUse;
        }


        public override decimal ImperialValue()
        {
            if (unitsToUse == Units.Imperial)
            {
                return mostRecentMeasure;
            }
            return Convert.ToDecimal(mostRecentMeasure * 2.2046);
        }

        public override decimal MetricValue()
        {
            if (unitsToUse == Units.Metric)
            {
                return mostRecentMeasure;
            }
            return Convert.ToDecimal(mostRecentMeasure * 0.4536);
        }
    }
}
