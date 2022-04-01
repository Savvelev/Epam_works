using System;
using System.Threading;

namespace M07_Delegates_LambdasAndEvents.Countdowns
{
    class Countdown 
    {
        public event EventHandler<MovingEventArgs> SomeEvent;

        public void StartCountdown(int millisecondsTimeout)
        {
            if (millisecondsTimeout < 0)
                throw new ArgumentOutOfRangeException("Entered a negative number");

            Thread.Sleep(millisecondsTimeout);

            SomeEvent?.Invoke(this, new MovingEventArgs("You are subsribed"));         
        }      
    }
}
