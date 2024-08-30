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
    /// Extends BinaryWriter to add extra handling for endianness.
    /// </summary>
    internal class EDBWriter : BinaryWriter
    {
        private readonly Endian Endian;

        public EDBWriter(FileStream stream, Encoding encoding, Endian Endian) : base(stream, encoding, false)
        {
            this.Endian = Endian;
        }

        public override void Write(short value)
        {
            if (Endian == Endian.Big)
            {
                base.Write(ByteSwapper.SwapBytes((ushort)value));
            } else
            {
                base.Write(value);
            }
        }

        public override void Write(ushort value)
        {
            if (Endian == Endian.Big)
            {
                base.Write(ByteSwapper.SwapBytes(value));
            }
            else
            {
                base.Write(value);
            }
        }

        public override void Write(int value)
        {
            if (Endian == Endian.Big)
            {
                base.Write(ByteSwapper.SwapBytes((uint)value));
            }
            else
            {
                base.Write(value);
            }
        }

        public override void Write(uint value)
        {
            if (Endian == Endian.Big)
            {
                base.Write(ByteSwapper.SwapBytes(value));
            }
            else
            {
                base.Write(value);
            }
        }
    }
}
