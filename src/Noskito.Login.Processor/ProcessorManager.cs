using System;
using System.Collections.Generic;
using System.Linq;

namespace Noskito.Login.Processor
{
    public class ProcessorManager
    {
        private readonly Dictionary<Type, IPacketProcessor> processors;

        public ProcessorManager(IEnumerable<IPacketProcessor> processors)
        {
            this.processors = processors.ToDictionary(x => x.PacketType);
        }

        public IPacketProcessor GetPacketProcessor(Type packetType)
        {
            return processors.GetValueOrDefault(packetType);
        }
    }
}