using CustomParser;
using NUnit.Framework;
using System.Runtime.CompilerServices;
using Moq;
using Microsoft.Extensions.Logging;
using System;

[assembly: InternalsVisibleTo("CustomParser")]

namespace CustomParses.Tests
{
    [TestFixture]
    internal class StringParserTests
    {

        private static readonly Mock<ILogger<StringParser>> mock = new();
        private static readonly StringParser testClass = new(mock.Object);

        [TestCase("345")]
       public void ParseFromStringToInt_String_True(string testCase)
       {         
            var expected = 345;
            Assert.That(() => testClass.ParseFromStringToInt(testCase), Is.EqualTo(expected));
       }

        [TestCase(null)]
       public void ParseFromStringToInt_EmptyOrNull_ArgumentNullException(string testCase)
       {          
            Assert.That(() => testClass.ParseFromStringToInt(testCase), Throws.ArgumentNullException);
       } 

        [TestCase("testString")]
       public void ParseFromStringToInt_NotDigits_ArgumentException(string testCase)
       {           
            Assert.That(() => testClass.ParseFromStringToInt(testCase), Throws.ArgumentException);
       }

        [TestCase("2147483648")]
       public void ParseFromStringToInt_OverRangeNum_OverflowException(string testCase)
       {       
            Assert.That(() => testClass.ParseFromStringToInt(testCase), Throws.TypeOf(typeof(OverflowException)));
       }
    }
}