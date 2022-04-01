using System.Linq;

namespace M07_Delegates_LambdasAndEvents.MatrixSorting
{
    class BubbleSortByMin
    {
        public double[][] Sorting(double[][] array, bool isAscending)
        {
            for (int partindex = array.Length - 1; partindex > 0; partindex--)
            {
                for (int i = 0; i < partindex; i++)
                {
                    if (isAscending ? array[i].Min() > array[i + 1].Min() : array[i].Min() < array[i + 1].Min())
                    {
                        Swaping.Swap(array, i);
                    }
                }
            }
            return array;
        }
    }
}
