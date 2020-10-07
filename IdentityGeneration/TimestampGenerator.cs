using System;

namespace IdentityGeneration
{
    internal class TimestampGenerator : ITimestampGenerator
    {
        public long GetTimestamp()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds() - 1595116800; //Custom epoch
        }
    }
}
