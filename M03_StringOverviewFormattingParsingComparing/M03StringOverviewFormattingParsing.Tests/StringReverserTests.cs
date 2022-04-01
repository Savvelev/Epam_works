using NUnit.Framework;

namespace M03_StringOverviewFormattingParsingComparing
{
    [TestFixture]
    internal class StringReverserTests
    {

        [TestCase(null)]
        public void ReverseString_Null_ArgumentException(string testCase)
        {
            Assert.That(() => StringReverser.ReverseString(testCase), Throws.ArgumentException);
        }

        [TestCase("")]
        public void ReverseString_Empty_ArgumentException(string testCase)
        {
            Assert.That(() => StringReverser.ReverseString(testCase), Throws.ArgumentException);
        }

        [TestCase(" ")]
        public void ReverseString_WhireSpace_ArgumentException(string testCase)
        {
            Assert.That(() => StringReverser.ReverseString(testCase), Throws.ArgumentException);
        }

        [TestCase("The greatest victory is that which requires no battle")]
        public void ReverseString_InputString_True(string testCase)
        {
            var expected = "battle no requires which that is victory greatest The";
            Assert.That(() => StringReverser.ReverseString(testCase), Is.EqualTo(expected));
        }
    }  
}
