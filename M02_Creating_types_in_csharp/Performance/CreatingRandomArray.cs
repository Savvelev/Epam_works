using System;

namespace Performance
{
    internal class CreatingRandomArray 
    {
        static readonly Random random = new();
        public static T[] CreateRandomArray<T>(int n)
            where T : IField, new()
        {
            var array = new T[n];

            for (int j = 0; j < n; j++)
            {
                var c = new T();
                c.I = SetRandomI();
                array[j] = c;
            }

            return array;
        }
        public static int SetRandomI()
        {
            return random.Next(10);
        }
    }
}