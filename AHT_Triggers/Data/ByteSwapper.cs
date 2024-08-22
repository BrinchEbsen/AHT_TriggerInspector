using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_Triggers.Data
{
    internal static class ByteSwapper
    {
        public static ushort SwapBytes(ushort x)
        {
            return (ushort)((ushort)((x & 0xff) << 8) | ((x >> 8) & 0xff));
        }

        public static uint SwapBytes(uint x)
        {
            return ((x & 0x000000ff) << 24) +
                   ((x & 0x0000ff00) << 8) +
                   ((x & 0x00ff0000) >> 8) +
                   ((x & 0xff000000) >> 24);
        }

        public static ulong SwapBytes(ulong value)
        {
            ulong uvalue = value;
            ulong swapped =
                  ((0x00000000000000FF) & (uvalue >> 56)
                 | (0x000000000000FF00) & (uvalue >> 40)
                 | (0x0000000000FF0000) & (uvalue >> 24)
                 | (0x00000000FF000000) & (uvalue >> 8)
                 | (0x000000FF00000000) & (uvalue << 8)
                 | (0x0000FF0000000000) & (uvalue << 24)
                 | (0x00FF000000000000) & (uvalue << 40)
                 | (0xFF00000000000000) & (uvalue << 56));
            return swapped;
        }

        public static float UintToSingle(uint x)
        {
            byte[] bytes = new byte[]{
                            (byte) (x & 0x000000FF),
                            (byte)((x & 0x0000FF00) >> 8 ),
                            (byte)((x & 0x00FF0000) >> 16),
                            (byte)((x & 0xFF000000) >> 24)
                        };

            return BitConverter.ToSingle(bytes, 0);
        }
    }
}
