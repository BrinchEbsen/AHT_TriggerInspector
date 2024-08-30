using AHT_Triggers.Common;
using AHT_Triggers.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AHT_Triggers.Data
{
    /// <summary>
    /// Bad name, I know.
    /// Extends BinaryReader to add extra handling for endianness.
    /// </summary>
    internal class EDBReader : BinaryReader
    {
        private readonly Endian Endian;

        public EDBReader(FileStream stream, Encoding encoding, Endian Endian) : base(stream, encoding, false) {
            this.Endian = Endian;
        }

        public override ushort ReadUInt16()
        {
            if (Endian == Endian.Big)
            {
                return ByteSwapper.SwapBytes(base.ReadUInt16());
            }
            else
            {
                return base.ReadUInt16();
            }
        }

        public override short ReadInt16()
        {
            return (short)ReadUInt16();
        }

        public override uint ReadUInt32()
        {
            if (Endian == Endian.Big)
            {
                return ByteSwapper.SwapBytes(base.ReadUInt32());
            } else
            {
                return base.ReadUInt32();
            }
        }

        public override int ReadInt32()
        {
            return (int)ReadUInt32();
        }

        public override float ReadSingle()
        {
            if (Endian == Endian.Big)
            {
                uint n = ReadUInt32();

                byte[]  bytes = new byte[]{
                    (byte) (n & 0x000000FF),
                    (byte)((n & 0x0000FF00) >> 8 ),
                    (byte)((n & 0x00FF0000) >> 16),
                    (byte)((n & 0xFF000000) >> 24)
                };

                return BitConverter.ToSingle(bytes, 0);
            } else
            {
                return base.ReadSingle();
            }
        }

        /// <summary>
        /// Reads a 4-byte relative pointer from the current stream, wraps it in a EXRelPtr object
        /// and advances the current position of the stream by 4 bytes.
        /// </summary>
        /// <returns>
        /// EXRelPtr object containing its own position and its value.
        /// </returns>
        public EXRelPtr ReadRelPtr()
        {
            int val = ReadInt32();

            return new EXRelPtr(val, (int)BaseStream.Position-4);
        }
    }
}
