using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M07_Delegates_LambdasAndEvents.Countdowns
{
    class MovingEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public MovingEventArgs(string message)
        {
            Message = message;
        }       
    }
}
