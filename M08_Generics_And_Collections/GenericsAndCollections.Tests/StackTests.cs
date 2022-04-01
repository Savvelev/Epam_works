using NUnit.Framework;

namespace GenericsAndCollections.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void IsEmpty_EmptyStack_True()
        {
            var stack = new Stack<int>();
            Assert.IsTrue(stack.IsEmpty);
        }
        
        [Test]
        public void Count_PushOneItem_True()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            Assert.AreEqual(1, stack.Count);
            Assert.IsFalse(stack.IsEmpty);
        }

        [Test]
        public void Pop_EmptyStack_InvalidOpertionException()
        {
            var stack = new Stack<int>();
            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_PushTwoItems_HeadElement()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            Assert.AreEqual(2, stack.Peek());
        }
        [Test]
        public void Peek_PushTwoItemsAndPop_HeadElement()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Pop();
            Assert.AreEqual(1, stack.Peek());
        }
    }
}