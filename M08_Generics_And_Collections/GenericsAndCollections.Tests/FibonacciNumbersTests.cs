using NUnit.Framework;
using System.Collections.Generic;


namespace GenericsAndCollections.Tests
{
    [TestFixture]
    class FibonacciNumbersTests
    {
        FibonacciNumbers fibonacciNumbers = new();    
        
        [TestCase((uint)5)]
        public void FibonacciNum_Num_True(uint testCase)
        {
            var list = new List<int> { 0, 1, 1, 2, 3, 5 };

            Assert.That(() => fibonacciNumbers.FibonacciNum(testCase), Is.EqualTo(list));
        }
    }
}
