using IdentityGeneration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace rIdentityGenetation.Tests
{
    public class ServiceCollectionTests
    {
        private ServiceCollection serviceCollection;

        [SetUp]
        public void Setup()
        {
            serviceCollection = new ServiceCollection();
        }

        [Test]
        public void RegistrationAddsAllServices()
        {
            serviceCollection.AddIdGenerator(1000);

            var provider = serviceCollection.BuildServiceProvider();

            Assert.IsInstanceOf<IIdGenerator>(provider.GetService<IIdGenerator>());
            Assert.IsInstanceOf<IdGenerator>(provider.GetService<IdGenerator>());
        }

        [Test]
        public void InvalidMachineIdThrows()
        {
            Assert.Throws<ArgumentException>(() => serviceCollection.AddIdGenerator(2000));
        }
    }
}
