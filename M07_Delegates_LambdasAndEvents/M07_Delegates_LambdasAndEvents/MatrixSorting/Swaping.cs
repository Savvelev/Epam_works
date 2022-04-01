namespace M07_Delegates_LambdasAndEvents.MatrixSorting
{
    internal class Swaping
    {
        public static void Swap(double[][] array, int i)
        {
            double[] temp = array[i];
            array[i] = array[i + 1];
            array[i + 1] = temp;
        }
    }
}