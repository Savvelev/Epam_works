using System;
using M07_Delegates_LambdasAndEvents.MatrixSorting;
using M07_Delegates_LambdasAndEvents.Countdowns;
using M07_Delegates_LambdasAndEvents.Countdowns.Subscribers;

namespace M07_Delegates_LambdasAndEvents
{
    class Program
    {
        static double[][] hardcodeDemo_matrix =
        {
            new double[6] { 120, 30, 40, 10, 70, 60 },
            new double[8] { -1, 2, 3, -10000, 1, 1, 1, 1 },
            new double[4] { 6, 4, 1000, 3 }
        };
        static void Main(string[] args)
        {                      
            var newSortStrategy = new SortStrategy();
            newSortStrategy.Sort(new BubbleSortByMax().Sorting, hardcodeDemo_matrix, true);
            DisplayMatrix(hardcodeDemo_matrix);         

            var countDown = new Countdown();
            countDown.SomeEvent += Subscriber1.SubToSomeEvent;
            countDown.SomeEvent += Subscriber2.SubToSomeEvent;

            countDown.StartCountdown(1000);

            countDown.SomeEvent -= Subscriber1.SubToSomeEvent;
            countDown.SomeEvent -= Subscriber2.SubToSomeEvent;
        }

        static void DisplayMatrix(double[][] hardcodeDemo_matrix)
        {
            foreach (var item in hardcodeDemo_matrix)
            {
                foreach (var i in item)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------");
        }       
    }  
}
