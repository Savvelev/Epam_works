using System;
using System.Collections;

namespace GenericsAndCollections
{
    public static class BinarySearching
    {
        public static int BinarySearch<T>(this IList collection, T item) where T: IComparable
        {
            if (collection == null || item == null || item.Equals(0))
                throw new ArgumentNullException("Reference is null");

            var min = 0;
            var max = collection.Count - 1;

            while (min<=max)
            {
                var middle = (min + max) / 2;

                if (item.CompareTo(collection[middle]) == 0)
                {
                    return middle;
                }
                else if (item.CompareTo(collection[middle]) >0)
                {
                    min = middle + 1;
                }
                else
                {
                    max = middle - 1;
                }
            }
            return -1;         
        }
    }
}
