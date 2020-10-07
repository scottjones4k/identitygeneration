using IdentityGeneration;
using Moq;
using NUnit.Framework;
using System;

namespace IdentityGenetation.Tests
{
    public class IdGeneratorTests
    {
        private Mock<ICountGenerator> countGenerator;
        private IdGenerator generator;
        private int machineId;
        private long timestamp;
        private int count;

        [SetUp]
        public void Setup()
        {
            var rand = new Random();
            countGenerator = new Mock<ICountGenerator>();
            machineId = rand.Next(1024);
            timestamp = rand.Next(500000);
            count = rand.Next(4096);
            generator = new IdGenerator(countGenerator.Object, machineId);

            countGenerator.Setup(s => s.Generate()).Returns((count, timestamp));
        }

        [Test]
        public void GeneratesExpectedId()
        {
            var expected = (timestamp << 22) | (machineId << 12) | count;

            var result = generator.Generate();

            Assert.AreEqual(expected, result);
        }
    }
}