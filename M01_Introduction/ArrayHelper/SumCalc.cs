
namespace ArrayHelper
{
    public class SumCalc
    {
        public static double PositiveSummator(double[,] array) 
        {
            double sum = 0;

            foreach (var item in array)
            {
                if (item>0)
                {
                    sum += item;
                }
            }

            return sum;
        }
    }
}
