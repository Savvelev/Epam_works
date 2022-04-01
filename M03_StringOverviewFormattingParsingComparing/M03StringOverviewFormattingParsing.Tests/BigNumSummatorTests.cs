using NUnit.Framework;

namespace M03_StringOverviewFormattingParsingComparing
{
    [TestFixture]
    class BigNumSummatorTests
    {
        [TestCase(null ,null)]
        public void Sum_Null_ArgumentNullException(string testCase1, string testCase2)
        {
            Assert.That(() => BigNumSummator.Sum(testCase1, testCase2), Throws.ArgumentNullException);
        }

        [TestCase("34f", "56")]
        public void Sum_NotDigits_ArgumentException(string testCase1, string testCase2)
        {
            Assert.That(() => BigNumSummator.Sum(testCase1, testCase2), Throws.ArgumentException);
        }

        [TestCase("123456789101112131415161718", "192021222324252627282930")]
        public void Sum_TwoBigString_True(string testCase1, string testCase2)
        {
            var expected = "123648810323436384042444648";
            Assert.That(() => BigNumSummator.Sum(testCase1, testCase2), Is.EqualTo(expected));
        }
    }
}
