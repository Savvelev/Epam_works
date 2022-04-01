using System;
using Moq;
using NUnit.Framework;
using M07_Delegates_LambdasAndEvents.Countdowns;


namespace M07_Delegates_LambdasAndEvents.Tests
{
    [TestFixture]
    class CountdownTests
    {
        private static readonly Mock<EventHandler<MovingEventArgs>> moqDelegate = new();

        [TestCase(1000)]
        public void StartCountdown_Time_InvokeEvent(int testCase)
        {
            //Arrange
            moqDelegate.Setup(x => x(It.IsAny<object>(), It.IsAny<MovingEventArgs>())).Verifiable();

            var countdown = new Countdown();
            countdown.SomeEvent += moqDelegate.Object;

            // Act
            countdown.StartCountdown(testCase);

            // Assert
            moqDelegate.Verify();
        }

        [TestCase(-11)]
        public void StartCountdown_NegativeNum_ArgumentOutOfRangeException(int testCase)
        {
            Assert.That(()=> new Countdown().StartCountdown(testCase), Throws.TypeOf(typeof(ArgumentOutOfRangeException)));
        }
    }
}
