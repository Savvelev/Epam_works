using NUnit.Framework;

namespace GenericsAndCollections.Tests
{
    [TestFixture]
    class QueueTests
    {        
        [Test]
        public void Capacity_EnQueueManyItems_DoubleCapacity()
        {
            var queue = new Queue<int>();
            queue.EnQueue(1);
            queue.EnQueue(2);
            queue.EnQueue(3);
            queue.EnQueue(4);
            queue.EnQueue(5);

            Assert.AreEqual(8, queue.Capacity);
        }

        [Test]
        public void Peek_EnQueueTwoItemsAndDeQueue_HeadElelement()
        {
            var queue = new Queue<int>();
            queue.EnQueue(1);
            queue.EnQueue(2);
            queue.DeQueue();

            Assert.AreEqual(2, queue.Peek());
        }

        [Test]
        public void IsEmpty_Empty_True()
        {
            var queue = new Queue<int>();

            Assert.IsTrue(queue.IsEmpty);
        }

        [Test]
        public void Count_EnQueueOneItem_OneItem()
        {
            var queue = new Queue<int>();
            queue.EnQueue(1);

            Assert.AreEqual(1, queue.Count);
            Assert.IsFalse(queue.IsEmpty);           
        }

        [Test]
        public void Peek_EmptyQueue_InvalidOpertionException()
        {
            var queue = new Queue<int>();

            Assert.That(() => queue.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void DeQueue_EmptyQueue_InvalidOpertionException()
        {
            var queue = new Queue<int>();

            Assert.That(() => queue.DeQueue(), Throws.InvalidOperationException);
        }

        [Test]
        public void DeQueue_DeQueueItems_True()
        {
            var queue = new Queue<int>();
            queue.EnQueue(1);
            queue.EnQueue(2);
            queue.EnQueue(3);
                    
            Assert.AreEqual(queue.DeQueue(), 1);
            Assert.AreEqual(queue.Count, 2);
            Assert.AreEqual(queue.DeQueue(), 2);
            Assert.AreEqual(queue.Count, 1);
            Assert.AreEqual(queue.DeQueue(), 3);
            Assert.AreEqual(queue.Count, 0);                               
        }

        [Test]
        public void EnQueue_EnQueueItems_True()
        {
            var queue = new Queue<int>();
            queue.EnQueue(1);
            Assert.AreEqual(queue.Count, 1);
            queue.EnQueue(2);
            Assert.AreEqual(queue.Count, 2);
            queue.EnQueue(3);
            Assert.AreEqual(queue.Count, 3);           
        }
    }
}
