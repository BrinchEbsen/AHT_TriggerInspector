﻿using AHT_Triggers.Common;
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

    /// <summary>
    /// A static object in a map with a position, rotation, scale and range.
    /// Has a type (and optional sub-type) that defines its behavior,
    /// along with a set of data and links to other triggers.
    /// </summary>
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

        public List<uint>   Data  = new List<uint>();
        public List<ushort> Links = new List<ushort>();

        public uint GfxHashRef;
        public uint GeoFileHashRef;
        public RGBA Tint;

        public uint ScriptIndex;
        public GameScript Script;

        /// <summary>
        /// Get the data at the specified index.
        /// </summary>
        /// <param name="index">Index into one of the 16 data slots.</param>
        /// <returns>The data.</returns>
        public uint? GetData(int index)
        {
            if (index < 0 || index >= 16) return null;
            if (!HasData(index)) return null;

            int j = 0; //Index into the "Data" list
            for (int i  = 0; i < 16; i++)
            {
                if (i == index)
                {
                    return Data[j];
                }

                //Increase index if this slot had data
                if (HasData(i)) j++;
            }

            return null;
        }
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

        /// <summary>
        /// Get the link at the specified index.
        /// </summary>
        /// <param name="index">Index into one of the 8 link slots.</param>
        /// <returns>Trigger index in the link slot.</returns>
        public ushort? GetLink(int index)
        {
            if (index < 0 || index >= 8) return null;
            if (!HasLink(index)) return null;

            int j = 0; //Index into the "Data" list
            for (int i = 0; i < 8; i++)
            {
                if (i == index)
                {
                    return Links[j];
                }

                //Increase index if this slot had data
                if (HasLink(i)) j++;
            }

            return null;
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
    }

    /// <summary>
    /// A list of instructions organized into procedures with a set of local/global variables.
    /// </summary>
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

    /// <summary>
    /// A logical set of instructions that can be called from other procedures. Has a defined start line and name.
    /// </summary>
    public struct Procedure
    {
        public int StartLine;
        public bool Blocked;
        public int Type;
        public string Name;
        public bool Exclusive;
        public int NumLocals;
    }

    /// <summary>
    /// An instruction with an ID and 4 units of data. The ID determines what the instruction does, with further behavior defined by the data.
    /// </summary>
    public struct CodeLine
    {
        public byte InstructionID;
        public byte Data1;
        public byte Data2;
        public byte Data3;
        public int  Data4;
    }
}
