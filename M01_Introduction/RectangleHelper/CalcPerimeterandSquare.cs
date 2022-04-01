using System;

namespace RectangleHelper
{
    public class CalcPerimeterandSquare
    {
        public static double Perimeter(double a, double b)
        {
            if (a <0 || b<0)
            {
                Console.WriteLine("I think you were wrong with the sigh,the side cannot be negative\n");
            }
            return Math.Abs((a + b) * 2);
        }
        public static double Square(double a, double b)
        {
            
            return Math.Abs((a * b));
        }
    }
}
