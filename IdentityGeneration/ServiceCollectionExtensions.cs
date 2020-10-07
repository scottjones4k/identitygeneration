using Microsoft.Extensions.DependencyInjection;
using System;

namespace IdentityGeneration
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add IdGenerator & IdGenerator for injection
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="machineId">Identity of the machine. Must be less than 1024</param>
        public static void AddIdGenerator(this IServiceCollection serviceCollection, int machineId)
        {
            if (machineId < 0 || machineId > 1023)
            {
                throw new ArgumentException("Must be between 0 and 1024", nameof(machineId));
            }

            serviceCollection.AddSingleton<ITimestampGenerator, TimestampGenerator>();
            serviceCollection.AddSingleton<ICountGenerator, CountGenerator>();
            serviceCollection.AddSingleton(services => new IdGenerator(services.GetService<ICountGenerator>(), machineId));
            serviceCollection.AddSingleton<IIdGenerator>(services => services.GetService<IdGenerator>());
        }
    }
}
