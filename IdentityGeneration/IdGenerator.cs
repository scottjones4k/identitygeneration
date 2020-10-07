using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityGeneration
{
    public class IdGenerator : IIdGenerator
    {
        private const int MachineIdBits = 10;
        private const int CountBits = 12;

        private readonly int _machineId;
        private readonly ICountGenerator _countGenerator;

        internal IdGenerator(ICountGenerator countGenerator, int machineId)
        {
            if (machineId < 0 || machineId > 1023) throw new ArgumentOutOfRangeException(nameof(machineId), machineId, "Machine id needs to be a positive integer below 1024");

            _machineId = machineId;
            _countGenerator = countGenerator;
        }

        public long Generate()
        {
            var (count, timestamp) = _countGenerator.Generate();

            var identifier = timestamp << (MachineIdBits + CountBits);
            identifier |= _machineId << CountBits;
            identifier |= count;

            return identifier;
        }
    }
}
