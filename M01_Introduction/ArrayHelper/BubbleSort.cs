
using System;

namespace ArrayHelper
{
    public class BubbleSort
    {
        public static void BubbleSorting(double[] array, bool isAscending)
        {                   
            for (int partindex = array.Length-1; partindex > 0; partindex--)
            {
                for (int i = 0; i < partindex; i++)
                {
                    if (isAscending ? array[i] > array[i + 1] : array[i] < array[i + 1])
                    {
                        Swap(array, i, i + 1);
                    }
                }
            }
        }


        private static void Swap (double[] array, int i, int j )
        {
            double temp;
            temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
