using System;
using System.Collections;
using ArrayHelper;
using RectangleHelper;

namespace Functional
{
    public class ArrayDim
    {
        public static double[,] Mas { get; set; }
    }
    internal class Functional
    {

        private static void Main(string[] args)
        {
            Start();
            Console.ReadLine();
        }

        public static void Start()
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1 - Bubble-sort an array\n" +
                              "2 - Calculate the sum of all positive elements in a two dimensional array\n" +
                              "3 - Calculate perimeter and square of rectangle");
            do
            {
                double input = ReadDouble();

                switch (input)
                {
                    case 1:
                        BubbleSortWay();
                        return;
                    case 2:
                        CalcPosNumsDimArr();
                        return;
                    case 3:
                        CalcPerAndSquare();
                        return;
                    default:
                        Console.WriteLine("Print 1 or 2 or 3");
                        continue;
                }
            } while (true);
        }

        public static void EnumerblePrinter(IEnumerable enumerable)
        {
            Console.Clear();
            Console.WriteLine("Sorted array:");
            foreach (var item in enumerable)
            {
                Console.Write($"{item} ");
            }
        }

        public static void BubbleSortWay()
        {
            Console.WriteLine("Enter a size of array");

            var n = (int)ReadDouble();
            var array = new double[n];

            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine("Enter a number");
                array[i] = ReadDouble();
            }

            Console.WriteLine("Done!");
            Console.WriteLine("Choose a way: 1 - ASC, 2 - DESC");
          
            while (true)
            {
                var sortingOption = (int)ReadDouble();

                switch (sortingOption)
                {
                    case 1:
                        BubbleSort.BubbleSorting(array, true);
                        break;
                    case 2:
                        BubbleSort.BubbleSorting(array, false);
                        break;
                    default:
                        Console.WriteLine("Enter 1 or 2");
                        continue;
                }               
                EnumerblePrinter(array);
                break;             
            }           
        }

        public static void CalcPosNumsDimArr()
        {
            Console.WriteLine("Enter a size of dimensional array:");
            Console.WriteLine("Columns:");
            var x = (int)ReadDouble();

            Console.WriteLine("Rows:");
            var y = (int)ReadDouble();

            ArrayDim.Mas = new double[x, y]; 
            Console.WriteLine("Enter a matrix");

            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    Console.Write("mas[" + i + "," + j + "]: ");
                    ArrayDim.Mas[i, j] = ReadDouble();
                }
            }

            Console.WriteLine("Sum of positive numbers from array:");
            Console.WriteLine(SumCalc.PositiveSummator(ArrayDim.Mas));
        }

        public static void CalcPerAndSquare()
        {
            Console.WriteLine("Enter one side of rectangle and other side");

            var a = ReadDouble();
            var b = ReadDouble();

            Console.WriteLine($"The perimeter equals: {CalcPerimeterandSquare.Perimeter(a, b)}");
            Console.WriteLine($"The square equals: {CalcPerimeterandSquare.Square(a, b)}");
        }

        public static double ReadDouble()
        {
            double resu;

            do
            {
                var str = Console.ReadLine();
                if (double.TryParse(str, out double result))
                {
                    resu = result;
                    break;
                }
            } while (true);

            return resu;
        }
    }
}
