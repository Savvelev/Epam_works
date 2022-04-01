using System;
using System.Collections;

namespace GenericsAndCollections
{
    public class FibonacciNumbers
    {
        public IEnumerable FibonacciNum(uint countOfNum)
        {           
            var previous = 0;
            var next = 1;
            
            for (int i = 0; i < countOfNum; i++)
            {
                if (i == 0)
                    yield return previous;

                Swap(ref previous, ref next);

                yield return previous;
            }
        }

        private static void Swap(ref int previous, ref int next)
        {
            var temp = previous;
            previous = next;
            next = temp + next;
        }
    }
}
