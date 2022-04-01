using System;

namespace M07_Delegates_LambdasAndEvents.Countdowns.Subscribers
{
    class Subscriber1
    {
        public static void SubToSomeEvent(object sender, MovingEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
