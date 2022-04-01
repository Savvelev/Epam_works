using NUnit.Framework;
using System;

namespace GenericsAndCollections.Tests
{
    [TestFixture]
    class PolishNotationTests
    {
        PolishNotation polishNotation = new();
        [TestCase("")]
        public void ReverseCalculator_LengthZero_Zero(string testCase)
        {
            Assert.That(() => polishNotation.ReverseCalculator(testCase), Is.EqualTo(0));
        }
        
        [TestCase("5 1 2 + 4 * + 3")]
        public void ReverseCalculator_WrongInputExpression_ArgumentException(string testCase)
        {
            Assert.That(() => polishNotation.ReverseCalculator(testCase), Throws.ArgumentException);
        }

        [TestCase("5 1 2 + 4 * + 3 -")]
        public void ReverseCalculator_InputExpression_True(string testCase)
        {
            Assert.That(() => polishNotation.ReverseCalculator(testCase), Is.EqualTo(14));
        }

        [TestCase("3 5 * 0 / 55 2 * - 10 +")]
        public void ReverseCalculator_DivideByZero_DivideByZeroException(string testCase)
        {
            Assert.That(() => polishNotation.ReverseCalculator(testCase), Throws.TypeOf(typeof(DivideByZeroException)));
        }

    }
    
}
