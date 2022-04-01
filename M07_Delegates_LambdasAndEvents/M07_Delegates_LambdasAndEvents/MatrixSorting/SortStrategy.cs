using System;

namespace M07_Delegates_LambdasAndEvents.MatrixSorting
{
    class SortStrategy
    {
        public double[][] Sort(Func<double[][], bool, double[][]> func, double[][] matrix, bool isAscening)
        {
            if (matrix == null)
                throw new ArgumentNullException("No input matrix");
            if (func == null)
                throw new ArgumentNullException("Delegate is null");
            return func.Invoke(matrix, isAscening);         
        }
    }
}
