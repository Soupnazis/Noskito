using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Noskito.Common.Logging;

namespace Noskito.World.Network.Pipeline
{
    public class Decoder : ByteToMessageDecoder
    {
        private readonly NetworkClient client;
        private readonly ILogger logger;

        public Decoder(ILogger logger, NetworkClient client)
        {
            this.logger = logger;
            this.client = client;
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

            if (client.EncryptionKey == 0)
                output.Add(DecryptCustomParameter(buffer));
            else
                output.AddRange(Decode(buffer));
        }

        private string DecryptPrivate(string str)
        {
            var receiveData = new List<byte>();
            char[] table = {' ', '-', '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'n'};
            for (var count = 0; count < str.Length; count++)
                if (str[count] <= 0x7A)
                {
                    int len = str[count];

                    for (var i = 0; i < len; i++)
                    {
                        count++;

                        try
                        {
                            receiveData.Add(unchecked((byte) (str[count] ^ 0xFF)));
                        }
                        catch
                        {
                            receiveData.Add(255);
                        }
                    }
                }
                else
                {
                    int len = str[count];
                    len &= 0x7F;

                    for (var i = 0; i < len; i++)
                    {
                        count++;
                        int highbyte;
                        try
                        {
                            highbyte = str[count];
                        }
                        catch
                        {
                            highbyte = 0;
                        }

                        highbyte &= 0xF0;
                        highbyte >>= 0x4;

                        int lowbyte;
                        try
                        {
                            lowbyte = str[count];
                        }
                        catch
                        {
                            lowbyte = 0;
                        }

                        lowbyte &= 0x0F;

                        if (highbyte != 0x0 && highbyte != 0xF)
                        {
                            receiveData.Add(unchecked((byte) table[highbyte - 1]));
                            i++;
                        }

                        if (lowbyte != 0x0 && lowbyte != 0xF) receiveData.Add(unchecked((byte) table[lowbyte - 1]));
                    }
                }

            return Encoding.UTF8.GetString(receiveData.ToArray());
        }

        public string DecryptCustomParameter(Span<byte> str)
        {
            try
            {
                var encryptedStringBuilder = new StringBuilder();
                for (var i = 1; i < str.Length; i++)
                {
                    if (Convert.ToChar(str[i]) == 0xE) return encryptedStringBuilder.ToString();

                    var firstbyte = Convert.ToInt32(str[i] - 0xF);
                    var secondbyte = firstbyte;
                    secondbyte &= 240;
                    firstbyte = Convert.ToInt32(firstbyte - secondbyte);
                    secondbyte >>= 4;

                    switch (secondbyte)
                    {
                        case 0:
                        case 1:
                            encryptedStringBuilder.Append(' ');
                            break;

                        case 2:
                            encryptedStringBuilder.Append('-');
                            break;

                        case 3:
                            encryptedStringBuilder.Append('.');
                            break;

                        default:
                            secondbyte += 0x2C;
                            encryptedStringBuilder.Append(Convert.ToChar(secondbyte));
                            break;
                    }

                    switch (firstbyte)
                    {
                        case 0:
                            encryptedStringBuilder.Append(' ');
                            break;

                        case 1:
                            encryptedStringBuilder.Append(' ');
                            break;

                        case 2:
                            encryptedStringBuilder.Append('-');
                            break;

                        case 3:
                            encryptedStringBuilder.Append('.');
                            break;

                        default:
                            firstbyte += 0x2C;
                            encryptedStringBuilder.Append(Convert.ToChar(firstbyte));
                            break;
                    }
                }

                return encryptedStringBuilder.ToString();
            }
            catch (OverflowException)
            {
                return string.Empty;
            }
        }

        private IEnumerable<string> Decode(Span<byte> str)
        {
            var encryptedString = new StringBuilder();

            var sessionKey = client.EncryptionKey & 0xFF;
            var sessionNumber = unchecked((byte) (client.EncryptionKey >> 6));
            sessionNumber &= 0xFF;
            sessionNumber &= 3;

            switch (sessionNumber)
            {
                case 0:
                    foreach (var character in str)
                    {
                        var firstbyte = unchecked((byte) (sessionKey + 0x40));
                        var highbyte = unchecked((byte) (character - firstbyte));
                        encryptedString.Append((char) highbyte);
                    }

                    break;

                case 1:
                    foreach (var character in str)
                    {
                        var firstbyte = unchecked((byte) (sessionKey + 0x40));
                        var highbyte = unchecked((byte) (character + firstbyte));
                        encryptedString.Append((char) highbyte);
                    }

                    break;

                case 2:
                    foreach (var character in str)
                    {
                        var firstbyte = unchecked((byte) (sessionKey + 0x40));
                        var highbyte = unchecked((byte) ((character - firstbyte) ^ 0xC3));
                        encryptedString.Append((char) highbyte);
                    }

                    break;

                case 3:
                    foreach (var character in str)
                    {
                        var firstbyte = unchecked((byte) (sessionKey + 0x40));
                        var highbyte = unchecked((byte) ((character + firstbyte) ^ 0xC3));
                        encryptedString.Append((char) highbyte);
                    }

                    break;

                default:
                    encryptedString.Append((char) 0xF);
                    break;
            }

            var temp = encryptedString.ToString().Split((char) 0xFF);

            var packets = new List<string>();

            for (var i = 0; i < temp.Length; i++) packets.Add(DecryptPrivate(temp[i]));

            return packets;
        }
    }
}