using System;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Noskito.Common.Logging;

namespace Noskito.World.Network.Pipeline
{
    public class Encoder : MessageToByteEncoder<string>
    {
        private readonly ILogger logger;

        public Encoder(ILogger logger)
        {
            this.logger = logger;
        }

        protected override void Encode(IChannelHandlerContext context, string message, IByteBuffer output)
        {
            if (!output.IsWritable())
            {
                logger.Debug("Output is not writable");
                return;
            }

            if (string.IsNullOrEmpty(message))
            {
                logger.Debug("Can't encode an empty or null string");
                return;
            }

            var bytes = Encoding.UTF8.GetBytes(message);
            var encryptedData = new byte[bytes.Length + (int) Math.Ceiling((decimal) bytes.Length / 126) + 1];

            var j = 0;
            for (var i = 0; i < bytes.Length; i++)
            {
                if (i % 126 == 0)
                {
                    encryptedData[i + j] = (byte) (bytes.Length - i > 126 ? 126 : bytes.Length - i);
                    j++;
                }

                encryptedData[i + j] = (byte) ~bytes[i];
            }

            encryptedData[^1] = 0xFF;
            output.WriteBytes(Unpooled.WrappedBuffer(encryptedData));
        }
    }
}