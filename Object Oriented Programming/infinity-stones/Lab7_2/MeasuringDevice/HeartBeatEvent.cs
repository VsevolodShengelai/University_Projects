using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuringDevice
{
    public class HeartBeatEventArgs : EventArgs
    {
        public readonly DateTime TimeStamp;
        public HeartBeatEventArgs() : base()
        {
            TimeStamp = DateTime.Now;
        }
    }
    public delegate void HeartBeatEventHandler(object sender, HeartBeatEventArgs args);
}
