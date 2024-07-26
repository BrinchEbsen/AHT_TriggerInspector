using AHT_Triggers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_Triggers.Data
{
    public struct MapTriggerData
    {
        public uint MapHash;
        public List<Trigger> TriggerList;
    }

    public class Trigger
    {
        public ushort TypeIndex;
        public EXHashCode Type;
        public EXHashCode SubType;

        public ushort Debug;
        public uint GameFlags;
        public uint TrigFlags;

        public EXVector3 Position;
        public EXVector3 Rotation;
        public EXVector3 Scale;

        public List<uint> Data = new List<uint>();
        public List<ushort> Links = new List<ushort>();

        public uint GfxHashRef;
        public uint GeoFileHashRef;
        public RGBA Tint;

        public uint ScriptIndex;
        public GameScript Script;

        public bool HasData(int index)
        {
            return (TrigFlags & (1 << index)) != 0;
        }
        public int CountData()
        {
            int count = 0;

            for (int i = 0; i < 16; i++)
            {
                if (HasData(i)) { count++; }
            }

            return count;
        }
        public bool HasLink(int index)
        {
            return (TrigFlags & (0x10000 << index)) != 0;
        }
        public int CountLinks()
        {
            int count = 0;

            for (int i = 0; i < 8; i++)
            {
                if (HasLink(i)) { count++; }
            }

            return count;
        }
        public bool HasGfxHashRef()
        {
            return (TrigFlags & 0x1000000) != 0;
        }
        public bool HasGeoFileHashRef()
        {
            return (TrigFlags & 0x2000000) != 0;
        }
        public bool HasScript()
        {
            return (TrigFlags & 0x4000000) != 0;
        }
        public bool HasTint()
        {
            return (TrigFlags & 0x10000000) != 0;
        }

        public string DecodeScript()
        {
            ByteCodeDecompiler decompiler = new ByteCodeDecompiler(this.Script);

            return decompiler.ScriptToString();
        }
    }

    public struct GameScript
    {
        public int NumLines;
        public int NumVars;
        public int NumGlobals;
        public int NumProcs;

        public string Name;
        public byte[] VTable;
        public List<Procedure> Procedures;
        public List<CodeLine> Code;
    }

    public struct Procedure
    {
        public int StartLine;
        public bool Blocked;
        public int Type;
        public string Name;
        public bool Exclusive;
        public int NumLocals;
    }

    public struct CodeLine
    {
        public byte InstructionID;
        public byte Data1;
        public byte Data2;
        public byte Data3;
        public int  Data4;
    }
}
