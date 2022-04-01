using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericsAndCollections
{
    public class Queue<T> : IEnumerable<T>
    {
        private T[] queue;
        private int head;
        private int tail;

        public Queue()
        {
            const int defaultCapacity = 4;
            queue = new T[defaultCapacity];
        }
        public Queue(int capacity)
        {
            queue = new T[capacity];
        }
        public int Count => tail - head;
        public bool IsEmpty => Count == 0;
        public int Capacity => queue.Length;

        public void EnQueue (T item)
        {
            if (queue.Length == tail)
            {
                T[] largerArray = new T[Count * 2];
                Array.Copy(queue, largerArray, Count);
                queue = largerArray;
            }
            queue[tail++] = item;
        }
        public T DeQueue()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Queue is empty");

            T item = queue[head];
            queue[head++] = default(T);
            
            if (IsEmpty)
                head = tail = 0;
            return item;
        }
        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException();
            return queue[head];
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (int i = head; i < tail; i++)
            {
                yield return queue[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
