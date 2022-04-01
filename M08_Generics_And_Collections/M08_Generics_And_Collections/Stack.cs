using System;
using System.Collections;
using System.Collections.Generic; 

namespace GenericsAndCollections
{
    public class Stack<T> : IEnumerable<T>
    {
        private T[] items;

        public Stack()
        {
            const int defaultCapacity = 4;
            items = new T[defaultCapacity];
        }
        public Stack(int capacity)
        {
            items = new T[capacity];
        }

        public int Count { get; private set; }
        public bool IsEmpty => Count == 0;

        public void Push(T item)
        {
            if (items.Length == Count)
            {
                T[] largeArray = new T[Count * 2];
                Array.Copy(items, largeArray, Count);

                items = largeArray;
            }
            items[Count++] = item;
        }

        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Stack is empty");
            T item = items[--Count];
            items[Count] = default; 
            return item;
        }

        public T Peek()
        {
            return items[Count - 1];
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = Count-1; i >= 0; i--)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
