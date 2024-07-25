using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_Triggers.Common
{
    public class EXRelPtr
    {
        public EXRelPtr(int Value, int SelfAddress) { 
            this.Value = Value;
            this.SelfAddress = SelfAddress;
        }

        private int Value { get; set; }
        private int SelfAddress { get; set; }

        public int GetAbsoluteAddress()
        {
            return Value + SelfAddress;
        }
    }

    public struct RGBA
    {
        public byte r, g, b, a;
    }

    public struct EXVector3
    {
        public float x, y, z;
    }

    public enum GamePlatform
    {
        None,
        PS2,
        GameCube,
        Xbox
    }

    public enum Endian
    {
        None,
        Little,
        Big
    }
}
