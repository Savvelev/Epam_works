using NUnit.Framework;

namespace M03_StringOverviewFormattingParsingComparing
{
    [TestFixture]
    internal class AverageWordLengthDefinerTests
    {
        [Test]
        public void DefineAverageWordLength_NotEmptyString_True()
        {
           //Arange
            var inputString = "Hello My name is Shark";
            var expected = 3.6;

            // Act
            var actualLength = AverageWordLengthDefiner.DefineAverageWordLength(inputString);

            //Assert;          
            Assert.That(actualLength, Is.EqualTo(expected));
        }
        
        [TestCase (null)]
        public void DefineAverageWordLength_Null_ArgumentNullException(string testCase)
        {
            Assert.That(() => AverageWordLengthDefiner.DefineAverageWordLength(testCase), Throws.ArgumentNullException);
        }
        [TestCase("")]
        public void DefineAverageWordLength_Empty_ArgumentException(string testCase)
        { 
            Assert.That(() => AverageWordLengthDefiner.DefineAverageWordLength(testCase), Throws.ArgumentException);
        }
        [TestCase("  ")]
        public void DefineAverageWordLength_WhiteSpace_ArgumentException(string testCase)
        { 
            Assert.That(() => AverageWordLengthDefiner.DefineAverageWordLength(testCase), Throws.ArgumentException);
        }
    }
}
