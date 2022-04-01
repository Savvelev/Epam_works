using System;
using System.Collections.Generic;
using GenericsAndCollections;

namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>()
            { 1,2,3,4,5,6,7,8,9,10};
            var a = list.BinarySearch(9);
            Console.WriteLine(a);


            foreach (var item in new FibonacciNumbers().FibonacciNum(5))
            {
                Console.WriteLine(item);
            }

            var stack = new GenericsAndCollections.Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }

            var pol = new PolishNotation();
            Console.WriteLine(pol.ReverseCalculator("5 1 2 + 4 * + 3 -"));
            Console.WriteLine(pol.ReverseCalculator("3 5 * 4 / 55 2 * - 10 +"));

            var queue = new GenericsAndCollections.Queue<int>();
            queue.EnQueue(1);
            queue.EnQueue(2);
            queue.EnQueue(3);
            queue.DeQueue();

            Console.WriteLine(queue.Peek());
            Console.WriteLine(queue.DeQueue());
            Console.WriteLine(queue.DeQueue());

        }
    }
}
