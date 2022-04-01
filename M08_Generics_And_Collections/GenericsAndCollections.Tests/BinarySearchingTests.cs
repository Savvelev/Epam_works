using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericsAndCollections.Tests
{
    [TestFixture]
    class BinarySearchingTests
    {        
        
        [TestCase(null, null)]       
        public void BinarySearch_Null_ArgumentNullException_1(IList list, int item)
        {
            Assert.That(() => BinarySearching.BinarySearch(list, 1), Throws.ArgumentNullException);
            Assert.That(() => BinarySearching.BinarySearch(new List<double> { 1, 2, 3 }, item), Throws.ArgumentNullException);
        }
           
        [Test]
        public void BinarySearch_SortedArray_True()
        {
            //Arange
            var rnd = new Random();
            var list = new List<int>();
            var testRange = 15;
            var includedInListTestValue = rnd.Next(0, testRange);

            var item = 0;
            for (int i = 0; i < testRange; i++)
            {                            
                list.Add(rnd.Next(-100, 100));
                if (i == includedInListTestValue)
                    item = list[i];
            }
            list.Sort();

            //Act + Assert
            Assert.That(() => BinarySearching.BinarySearch(list, item), Is.EqualTo(list.IndexOf(item)));
        }
    }           
}
