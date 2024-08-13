using AHT_Triggers.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AHT_Triggers.Data
{
    public class FileHandler
    {
        /// <summary>
        /// Returns which platform the GeoFile is made for, and throws an exception if it could not be determined.
        /// </summary>
        public static GamePlatform CheckPlatform(string filename)
        {
            GamePlatform Platform;

            using (var stream = File.Open(filename, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    //Check endian
                    Endian Endian;

                    char[] MagicValue = reader.ReadChars(4);
                    string s = new string(MagicValue);

                    if (s == "GEOM")
                    {
                        //Console.WriteLine("File identified as big endian");
                        Endian = Endian.Big;
                    }
                    else if (s == "MOEG")
                    {
                        //Console.WriteLine("File identified as little endian");
                        Endian = Endian.Little;
                    }
                    else
                    {
                        Console.WriteLine("Invalid file: Magic value read: " + s);
                        throw new IOException("Invalid GeoHeader.");
                    }

                    //Check EDB version
                    int EDBVersion;

                    if (Endian == Endian.Big)
                    {
                        stream.Seek(11, SeekOrigin.Begin);
                    }
                    else
                    {
                        stream.Seek(8, SeekOrigin.Begin);
                    }

                    EDBVersion = reader.ReadByte();

                    if (EDBVersion != 0xF0) //240 is final
                    {
                        throw new IOException("Non-final GeoFile version! 240 expected, read " + EDBVersion);
                    }

                    //Check platform
                    stream.Seek(0x1c, SeekOrigin.Begin);

                    int PlatformValue = reader.ReadInt32();

                    if (PlatformValue == 0 && Endian == Endian.Big)
                    {
                        Platform = GamePlatform.GameCube;
                    }
                    else if (PlatformValue == 1 && Endian == Endian.Little)
                    {
                        Platform = GamePlatform.Xbox;
                    }
                    else if (PlatformValue == 0 && Endian == Endian.Little)
                    {
                        Platform = GamePlatform.PS2;
                    }
                    else
                    {
                        Console.WriteLine("Could not determine platform of " + filename);
                        throw new IOException("Could not determine game version.");
                    }

                    //Console.WriteLine("File identified as " + Platform.ToString());
                }
            }

            return Platform;
        }

        public static List<MapTriggerData> ReadTriggerData(string filePath, GamePlatform platform)
        {
            List<MapTriggerData> maps = new List<MapTriggerData>();

            using (var stream = File.Open(filePath, FileMode.Open))
            {
                //Determine encoding
                Endian Endian = platform == GamePlatform.GameCube ? Endian.Big : Endian.Little;
                Encoding Encoding = Endian == Endian.Big ? Encoding.BigEndianUnicode : Encoding.Unicode;

                using (var reader = new EDBReader(stream, Encoding, Endian))
                {
                    //Read map information in the GeoHeader
                    stream.Seek(0x84, SeekOrigin.Begin);
                    int numMaps = reader.ReadInt16();
                    if (numMaps == 0)
                    {
                        Console.WriteLine("No maps in file.");
                        throw new IOException("No maps found in geofile!");
                    }
                    //Console.WriteLine("Found "+numMaps+" maps");

                    //Read pointer to map headers
                    stream.Seek(0x2, SeekOrigin.Current);
                    int pMapHeaders = reader.ReadRelPtr().GetAbsoluteAddress();

                    //Loop trough all maps and add it to the list
                    for (int i = 0; i < numMaps; i++)
                    {
                        stream.Seek(pMapHeaders + (0x10 * i), SeekOrigin.Begin);

                        MapTriggerData map = new MapTriggerData();

                        //Read hash
                        map.MapHash = reader.ReadUInt32();

                        //Read GeoMap address
                        stream.Seek(0x4, SeekOrigin.Current);
                        int pGeoMap = reader.ReadInt32();
                        //Console.WriteLine(string.Format("pGeoMap: {0:X}", pGeoMap));

                        //Read Triggerheader address
                        stream.Seek(pGeoMap + 0x58, SeekOrigin.Begin);
                        int pTriggerHeader = reader.ReadRelPtr().GetAbsoluteAddress();
                        //Console.WriteLine(string.Format("pTriggerHeader: {0:X}", pTriggerHeader));

                        //Read pointers to trigger data
                        stream.Seek(pTriggerHeader, SeekOrigin.Begin);
                        int numTriggers = reader.ReadInt32();
                        int pTriggerList = reader.ReadRelPtr().GetAbsoluteAddress();
                        int pTriggerScripts = reader.ReadRelPtr().GetAbsoluteAddress();
                        int pTriggerTypes = reader.ReadRelPtr().GetAbsoluteAddress();

                        //Console.WriteLine("Number of triggers: "+numTriggers);

                        //Loop through all triggers
                        map.TriggerList = new List<Trigger>();
                        for (int j = 0; j < numTriggers; j++)
                        {
                            Trigger trigger = new Trigger();

                            //Read trigger pointer
                            stream.Seek(pTriggerList + (0x8 * j), SeekOrigin.Begin);
                            //Console.WriteLine(string.Format("Read from Triggerlist at: {0:X}", stream.Position));
                            int pTrigger = reader.ReadRelPtr().GetAbsoluteAddress();
                            //Console.WriteLine(string.Format("pTrigger: {0:X}", pTrigger));

                            //Read trigger information
                            stream.Seek(pTrigger, SeekOrigin.Begin);
                            trigger.TypeIndex = reader.ReadUInt16();
                            trigger.Debug = reader.ReadUInt16();
                            trigger.GameFlags = reader.ReadUInt32();
                            trigger.TrigFlags = reader.ReadUInt32();
                            trigger.Position.x = reader.ReadSingle();
                            trigger.Position.y = reader.ReadSingle();
                            trigger.Position.z = reader.ReadSingle();
                            trigger.Rotation.x = reader.ReadSingle();
                            trigger.Rotation.y = reader.ReadSingle();
                            trigger.Rotation.z = reader.ReadSingle();
                            trigger.Scale.x = reader.ReadSingle();
                            trigger.Scale.y = reader.ReadSingle();
                            trigger.Scale.z = reader.ReadSingle();

                            //Read trigger data.
                            //ALWAYS 32-bit, even if it gets saved as a short
                            for (int k = 0; k < 16; k++)
                            {
                                if (trigger.HasData(k))
                                {
                                    trigger.Data.Add(reader.ReadUInt32());
                                }
                            }
                            for (int k = 0; k < 8; k++)
                            {
                                if (trigger.HasLink(k))
                                {
                                    trigger.Links.Add((ushort)reader.ReadUInt32());
                                }
                            }
                            if (trigger.HasGfxHashRef())
                            {
                                trigger.GfxHashRef = reader.ReadUInt32();
                            }
                            if (trigger.HasGeoFileHashRef())
                            {
                                trigger.GeoFileHashRef = reader.ReadUInt32();
                            }
                            if (trigger.HasScript())
                            {
                                trigger.ScriptIndex = reader.ReadUInt32();
                            }
                            if (trigger.HasTint())
                            {
                                trigger.Tint.r = reader.ReadByte();
                                trigger.Tint.g = reader.ReadByte();
                                trigger.Tint.b = reader.ReadByte();
                                trigger.Tint.a = reader.ReadByte();
                            }

                            //Read from the triggertypes table
                            stream.Seek(pTriggerTypes + (trigger.TypeIndex * 0x10), SeekOrigin.Begin);
                            trigger.Type = (EXHashCode)reader.ReadUInt32();
                            trigger.SubType = (EXHashCode)reader.ReadUInt32();

                            if (trigger.HasScript())
                            {
                                //Read from scripts table
                                stream.Seek(pTriggerScripts + (trigger.ScriptIndex * 0x8), SeekOrigin.Begin);
                                int pScriptData = reader.ReadRelPtr().GetAbsoluteAddress();
                                //Console.WriteLine(string.Format("ScriptIndex: {0}", trigger.ScriptIndex));
                                //Console.WriteLine(string.Format("pScriptData: {0:X}", pScriptData));

                                //Read all the script's data

                                trigger.Script.VTable = new byte[16];

                                stream.Seek(pScriptData + 0x4, SeekOrigin.Begin);
                                for (int k = 0; k < 16; k++)
                                {
                                    trigger.Script.VTable[k] = reader.ReadByte();
                                }

                                trigger.Script.NumLines = reader.ReadUInt16();

                                char[] scriptName = new char[7];
                                for (int k = 0; k < scriptName.Length; k++)
                                {
                                    scriptName[k] = (char)reader.ReadByte();
                                }
                                trigger.Script.Name = TrimCharArray(scriptName);

                                trigger.Script.NumVars = reader.ReadByte();
                                trigger.Script.NumGlobals = reader.ReadByte();
                                trigger.Script.NumProcs = reader.ReadByte();

                                //Read procedure data
                                stream.Seek(0x4, SeekOrigin.Current);
                                trigger.Script.Procedures = new List<Procedure>();
                                for (int k = 0; k < trigger.Script.NumProcs; k++)
                                {
                                    Procedure proc = new Procedure();

                                    proc.StartLine = reader.ReadUInt16();
                                    proc.Blocked = reader.ReadByte() != 0;
                                    proc.Type = reader.ReadByte();

                                    char[] procName = new char[10];
                                    for (int l = 0; l < procName.Length; l++)
                                    {
                                        procName[l] = (char)reader.ReadByte();
                                    }
                                    proc.Name = TrimCharArray(procName);

                                    proc.Exclusive = reader.ReadByte() != 0;
                                    proc.NumLocals = reader.ReadByte();

                                    trigger.Script.Procedures.Add(proc);
                                }

                                //Read all lines of code
                                trigger.Script.Code = new List<CodeLine>();
                                for (int k = 0; k < trigger.Script.NumLines; k++)
                                {
                                    CodeLine line = new CodeLine();

                                    line.InstructionID = reader.ReadByte();

                                    //debug
                                    if (line.InstructionID == 0x38)
                                    {
                                        Console.WriteLine("ARRAY FOUND IN SCRIPT #" + trigger.ScriptIndex.ToString());
                                    }

                                    line.Data1 = reader.ReadByte();
                                    line.Data2 = reader.ReadByte();
                                    line.Data3 = reader.ReadByte();
                                    line.Data4 = reader.ReadInt32();

                                    trigger.Script.Code.Add(line);
                                }
                            }

                            map.TriggerList.Add(trigger);
                        }

                        maps.Add(map);
                    }
                }
            }

            return maps;
        }

        public static string TrimCharArray(char[] chars)
        {
            int len = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == 0)
                {
                    break;
                }
                len++;
            }

            char[] newChars = new char[len];

            Array.Copy(chars, newChars, len);

            return new string(newChars);
        }
    }
}
