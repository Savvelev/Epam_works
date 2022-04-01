using NUnit.Framework;
using M07_Delegates_LambdasAndEvents.MatrixSorting;
using Moq;
using System;

namespace M07_Delegates_LambdasAndEvents.Tests
{
    [TestFixture]
    public class SortStrategyTests
    {
        double[][] testMatrix =
        {
            new double[6] { 120, 30, 40, 10, 70, 60 },
            new double[8] { -1, 2, 3, -10000, 1, 1, 1, 1 },
            new double[4] { 6, 4, 1000, 3 }
        };
        readonly SortStrategy sortStrategyTest = new();

        [TestCase(null,null)]
        public void Sort_Null_ArgumentNullException(double[][] testCase, Func<double[][],bool,double[][]> funcTest)
        {
            Mock<BubbleSortByMax> moqDelegate = new(); 
            Assert.That(() => sortStrategyTest.Sort(moqDelegate.Object.Sorting, testCase, false), Throws.ArgumentNullException);
            Assert.That(() => sortStrategyTest.Sort(funcTest, testMatrix, false), Throws.ArgumentNullException);
        }   

        [TestCase(true)]
        public void Sort_ByMax_Ascending_True(bool Ascending)
        {                   
            testMatrix[0] = new double[8] { -1, 2, 3, -10000, 1, 1, 1, 1 };
            testMatrix[1] = new double[6] { 120, 30, 40, 10, 70, 60 };
            testMatrix[2] = new double[4] { 6, 4, 1000, 3 };

            Assert.That(() => sortStrategyTest.Sort(new BubbleSortByMax().Sorting, testMatrix, Ascending), Is.EqualTo(testMatrix));
        }

        [TestCase(true)]
        public void Sort_ByMix_Ascending_True(bool Ascending)
        {           
            testMatrix[0] = new double[8] { -1, 2, 3, -10000, 1, 1, 1, 1 };
            testMatrix[1] = new double[4] { 6, 4, 1000, 3 };
            testMatrix[2] = new double[6] { 120, 30, 40, 10, 70, 60 };

            Assert.That(() => sortStrategyTest.Sort(new BubbleSortByMin().Sorting, testMatrix, Ascending), Is.EqualTo(testMatrix));
        }
        
        [TestCase(false)]
        public void Sort_BySum_Descending_True(bool Ascending)
        {       
            testMatrix[0] = new double[4] { 6, 4, 1000, 3 };
            testMatrix[1] = new double[6] { 120, 30, 40, 10, 70, 60 };
            testMatrix[2] = new double[8] { -1, 2, 3, -10000, 1, 1, 1, 1 } ;

            Assert.That(() => sortStrategyTest.Sort(new BubbleSortBySum().Sorting, testMatrix, Ascending), Is.EqualTo(testMatrix));
        }       
    }
}