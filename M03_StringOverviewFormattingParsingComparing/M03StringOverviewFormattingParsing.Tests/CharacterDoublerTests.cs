using NUnit.Framework;

namespace M03_StringOverviewFormattingParsingComparing
{
    [TestFixture]
    class CharacterDoublerTests
    {
        [TestCase(null,"test")]
        public void DoubleCharacters_Null_ArgumentException(string testCase1 ,string testCase2)
        {
            Assert.That(() => CharacterDoubler.DoubleCharacters(testCase1 , testCase2), Throws.ArgumentException);
        }

        [TestCase("test","")]
        public void DoubleCharacters_Empty_ArgumentException(string testCase1, string testCase2)
        {
            Assert.That(() => CharacterDoubler.DoubleCharacters(testCase1, testCase2), Throws.ArgumentException);
        }

        [TestCase("omg i love shrek", "o kek")]
        public void DoubleCharacters_StringsToDoubleChars_True(string testCase1, string testCase2)
        {
            var expected = "oomg i loovee shreekk";
            Assert.That(() => CharacterDoubler.DoubleCharacters(testCase1, testCase2), Is.EqualTo(expected));
        }
    }
}
