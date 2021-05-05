using System;
using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Noskito.Common.Logging;

namespace Noskito.Login.Network.Pipeline
{
    public class Decoder : ByteToMessageDecoder
    {
        private readonly ILogger logger;

        public Decoder(ILogger logger)
        {
            this.logger = logger;
        }

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            if (!input.IsReadable())
            {
                logger.Debug("Input is not readable");
                return;
            }

            var buffer = new byte[input.ReadableBytes];

            input.ReadBytes(buffer);

            var packet = string.Empty;
            foreach (var b in buffer)
                packet += b > 14
                    ? Convert.ToChar((b - 0xF) ^ 0xC3)
                    : Convert.ToChar((0x100 - (0xF - b)) ^ 195);

            output.Add(packet.Trim());
        }
    }
}