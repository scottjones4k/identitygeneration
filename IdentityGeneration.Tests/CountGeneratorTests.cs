using IdentityGeneration;
using Moq;
using NUnit.Framework;
using System;

namespace IdentityGenetation.Tests
{
    public class CountGeneratorTests
    {
        private Mock<ITimestampGenerator> timestampGenerator;
        private CountGenerator generator;
        private long timestamp;
        private int callCount;

        [SetUp]
        public void Setup()
        {
            timestampGenerator = new Mock<ITimestampGenerator>();
            generator = new CountGenerator(timestampGenerator.Object);

            timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            callCount = 0;
            timestampGenerator.Setup(t => t.GetTimestamp()).Returns(() => { callCount++; return callCount > 4100 ? timestamp + 1 : timestamp; });
        }

        [Test]
        public void ReturnsTimestampAndCount()
        {
            var result = generator.Generate();
            Assert.AreEqual((0, timestamp), result);
        }

        [Test]
        public void CallingTwiceIncrementsCount()
        {
            generator.Generate();
            var result = generator.Generate();
            Assert.AreEqual((1, timestamp), result);
        }

        [Test]
        public void WaitsForNextMillisecond()
        {
            for (int i = 0;i < 4096;i++)
            {
                Assert.AreEqual(generator.Generate(), (i, timestamp));
            }

            var result = generator.Generate();

            Assert.AreEqual((0, timestamp+1), result);
        }
    }
}