using System;

namespace IdentityGeneration
{
    internal class CountGenerator : ICountGenerator
    {
        private const int CounterMax = 4095;
        private static readonly object Lock = new object { };

        private readonly ITimestampGenerator _timestampGenerator;

        private int Counter;
        private long LastTime;

        public CountGenerator(ITimestampGenerator timestampGenerator)
        {
            _timestampGenerator = timestampGenerator;
        }

        public (int, long) Generate() => GetComponents();

        private (int, long) GetComponents()
        {
            int localCount;
            long localTimestamp;
            lock (Lock)
            {
                localTimestamp = _timestampGenerator.GetTimestamp();
                if (LastTime != localTimestamp)
                {
                    LastTime = localTimestamp;
                    localCount = Counter = 0;
                }
                else
                {
                    localCount = ++Counter;
                }

            }
            if (Counter <= CounterMax) return (localCount, localTimestamp);
            WaitNextMillisecond(localTimestamp);
            return GetComponents();
        }

        private long WaitNextMillisecond(long time)
        {
            long returntime;

            do
            {
                returntime = _timestampGenerator.GetTimestamp();
            } while (time == returntime);

            return returntime;
        }
    }
}
