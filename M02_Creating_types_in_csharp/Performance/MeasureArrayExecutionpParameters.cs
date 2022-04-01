using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Performance
{
    class MeasureArrayExecutionpParameters
    {
        public static TimeSpan CalculateTimeExecution(Action action)
        {
            var sw = new Stopwatch();

            sw.Start();
            action();
            sw.Stop();

            return sw.Elapsed;
        }

        public static long GetPMS64DeltaForArrayInit<T>(out T[] array)
            where T : IField, new()
        {
            long start;
            long end;

            start = Process.GetCurrentProcess().PrivateMemorySize64;
            array = CreatingRandomArray.CreateRandomArray<T>(100000);
            end = Process.GetCurrentProcess().PrivateMemorySize64;

            return end - start;
        }
    }
}
