using System;
using System.Collections.Generic;
using System.IO;

namespace AHT_Triggers.Data
{
    /// <summary>
    /// Contains a public static variable to store the saved info for the current session.
    /// Provides methods to load/save variable names from/to a file.
    /// </summary>
    public static class ScriptSaveInfoHandler
    {
        /// <summary>
        /// Active saved info object for the gamescript being viewed.
        /// Made static so as to be accessed from anywhere.
        /// </summary>
        public static GameScriptSaveInfo ActiveInfo = new GameScriptSaveInfo();

        /// <summary>
        /// Get the path to where the info for the script with the given info will be saved/loaded from.
        /// This is the app directory + a folder called "gs_saved" + the file name.
        /// The file name is formatted as "[fileName]_[mapIndex]_[scriptIndex].txt".
        /// </summary>
        /// <param name="fileName">Name of the .edb file that is being inspected</param>
        /// <param name="mapIndex">Index of the map being viewed</param>
        /// <param name="scriptIndex">Index of the gamescript in the map</param>
        /// <returns>Path to the save file associated with this gamescript.</returns>
        /// <exception cref="IOException">May be thrown when creating folder</exception>
        private static string GetVarNamesFilePath(string fileName, int mapIndex, int scriptIndex)
        {
            //Get the base directory of the app and add "gs_saved"
            string folderpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gs_saved");
            //Console.WriteLine(folderpath);

            //If the folder doesn't exist, create it.
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            //Format the file name correctly
            string txtfilename = string.Format("{0}_{1}_{2}.txt",
                fileName.Replace(".edb", ""),
                mapIndex,
                scriptIndex);

            //Combine directory and file name
            return Path.Combine(folderpath, txtfilename);
        }

        /// <summary>
        /// Reset the active info object to its initial state based on the script's automatically generated names.
        /// </summary>
        /// <param name="script">GameScript assocated with the info object</param>
        /// <returns>Initialized info object</returns>
        public static GameScriptSaveInfo InitialiseSaveInfo(GameScript script)
        {
            return InitialiseSaveInfo(script, ActiveInfo);
        }

        /// <summary>
        /// Reset the given info object to its initial state based on the script's automatically generated names.
        /// </summary>
        /// <param name="script">GameScript assocated with the info object</param>
        /// <param name="info">Info object</param>
        /// <returns>Initialized info object</returns>
        public static GameScriptSaveInfo InitialiseSaveInfo(GameScript script, GameScriptSaveInfo info)
        {
            //Clear the lists from the info object
            info.Clear();
            //Create decompiler which will auto-generate the script's names
            ByteCodeDecompiler decomp = new ByteCodeDecompiler(script);

            //Generate variables
            for (int i = 0; i < script.NumVars; i++)
            {
                info.AddVar(decomp.GenerateVarName(i));
            }

            //Generate procedures
            for (int i = 0; i < script.NumProcs; i++)
            {
                info.AddProc(decomp.GenerateProcName(i));
            }

            //Generate labels
            info.Labels = decomp.GenerateLabels();

            return info;
        }

        /// <summary>
        /// Load into the active info object the information stored in the .txt containing the previously saved names for things.
        /// If no file is found, the info object is initialized with the auto-generated names instead.
        /// </summary>
        /// <param name="fileName">Name of the .edb file being viewed</param>
        /// <param name="mapIndex">Index of the map being viewed</param>
        /// <param name="trigger">Trigger associated with the gamescript</param>
        /// <exception cref="IOException">May be thrown when reading file</exception>
        public static void LoadInfoFromFile(string fileName, int mapIndex, Trigger trigger)
        {
            LoadInfoFromFile(fileName, mapIndex, trigger, ActiveInfo);
        }

        /// <summary>
        /// Load into the given info object the information stored in the .txt containing the previously saved names for things.
        /// If no file is found, the info object is initialized with the auto-generated names instead.
        /// </summary>
        /// <param name="fileName">Name of the .edb file being viewed</param>
        /// <param name="mapIndex">Index of the map being viewed</param>
        /// <param name="trigger">Trigger associated with the gamescript</param>
        /// <param name="Info">Info file to load data into</param>
        /// <exception cref="IOException">May be thrown when reading file</exception>
        public static void LoadInfoFromFile(string fileName, int mapIndex, Trigger trigger, GameScriptSaveInfo Info)
        {
            //Get path to file
            string txtpath = GetVarNamesFilePath(fileName, mapIndex, (int)trigger.ScriptIndex);

            //If the file doesn't exist already, create a new list
            if (!File.Exists(txtpath))
            {
                InitialiseSaveInfo(trigger.Script, Info);
                return;
            }

            //Clear the list to be filled later
            Info.Clear();

            //Open file
            using (StreamReader reader = new StreamReader(
                        //Set FileShare to Read so other processes also access it
                        new FileStream(txtpath, FileMode.Open, FileAccess.Read, FileShare.Read)
                    )
                )
            {
                try
                {
                    //First line is just info so we skip it
                    reader.ReadLine();
                    string line = reader.ReadLine();
                    int varIndex = 0;
                    int procIndex = 0;

                    while (line != null)
                    {
                        //Check for different identifiers at the start of the given line
                        if (line.IndexOf("VAR") == 0)
                        {
                            //Get name after identifier
                            string n = line.Replace(string.Format("VAR #{0}: ", varIndex), "");
                            //If nothing changed, it's formatted wrong
                            if (n == line)
                            {
                                throw new FormatException("Marker for variable with index " + varIndex + " is incorrect. "
                                    + "Line read: " + line);
                            }
                            Info.AddVar(n);
                            varIndex++;
                        }
                        else if (line.IndexOf("PROC") == 0)
                        {
                            //Get name after identifier
                            string n = line.Replace(string.Format("PROC #{0}: ", procIndex), "");
                            //If nothing changed, it's formatted wrong
                            if (n == line)
                            {
                                throw new FormatException("Marker for procedure with index " + procIndex + " is incorrect. "
                                    + "Line read: " + line);
                            }
                            Info.AddProc(n);
                            procIndex++;
                        }
                        else if (line.IndexOf("LABEL") == 0)
                        {
                            //The number between these markers is the line number (key in our labels dictionary)
                            int i1 = line.IndexOf("#L");
                            int i2 = line.IndexOf(":");

                            //Add 2 to start index and subtract 2 from length to be after the #L
                            string s_lineNr = line.Substring(i1 + 2, i2 - i1 - 2);
                            int lineNr = int.Parse(s_lineNr);

                            string n = line.Replace(string.Format("LABEL #L{0}: ", lineNr), "");
                            //If nothing changed, it's formatted wrong
                            if (n == line)
                            {
                                throw new FormatException("Marker for label with line number " + lineNr + " is incorrect. "
                                    + "Line read: " + line);
                            }
                            Info.AddLabel(lineNr, n);
                        }

                        line = reader.ReadLine();
                    }

                    //Verify that the amount of elements found is correct
                    int numVars = trigger.Script.NumVars;
                    int numProcs = trigger.Script.NumProcs;
                    int numLabels = new ByteCodeDecompiler(trigger.Script).CountLabels();

                    //This should match - throw an IOException if not.
                    if (numVars != Info.NumVars())
                    {
                        throw new IOException("The number of variables defined in the save file "+txtpath+" does not match that of the gamescript.");
                    }
                    if (numProcs != Info.NumProcs())
                    {
                        throw new IOException("The number of procedures defined in the save file "+txtpath+" does not match that of the gamescript.");
                    }
                    if (numLabels != Info.NumLabels())
                    {
                        throw new IOException("The number of labels defined in the save file "+txtpath+" does not match that of the gamescript.");
                    }

                //Don't catch IOException because it should be handled elsewhere (to display errors and such)
                } catch (Exception ex) when (ex is FormatException | ex is ArgumentOutOfRangeException)
                {
                    //If a formatting error occours, report it via IOException.
                    throw new IOException("Save file "+txtpath+" is not formatted correctly: "+ex.Message);
                }
            }
        }

        /// <summary>
        /// Save the info in the active info object into the .txt containing the previously saved names for things.
        /// If no file is found, a new file will be created with the appropriate name.
        /// </summary>
        /// <param name="fileName">Name of the .edb file being viewed</param>
        /// <param name="mapIndex">Index of the map being viewed</param>
        /// <param name="trigger">Trigger associated with the gamescript</param>
        /// <exception cref="IOException">May be thrown when writing file</exception>
        public static void SaveInfoToFile(string fileName, int mapIndex, Trigger trigger)
        {
            SaveInfoToFile(fileName, mapIndex, trigger, ActiveInfo);
        }

        /// <summary>
        /// Save the info in the given info object into the .txt containing the previously saved names for things.
        /// If no file is found, a new file will be created with the appropriate name.
        /// </summary>
        /// <param name="fileName">Name of the .edb file being viewed</param>
        /// <param name="mapIndex">Index of the map being viewed</param>
        /// <param name="trigger">Trigger associated with the gamescript</param>
        /// <param name="Info">Info file from which to read data to be saved</param>
        /// <exception cref="IOException">May be thrown when writing file</exception>
        public static void SaveInfoToFile(string fileName, int mapIndex, Trigger trigger, GameScriptSaveInfo Info)
        {
            //Get path to file
            string txtpath = GetVarNamesFilePath(fileName, mapIndex, (int)trigger.ScriptIndex);

            //Open file
            using (StreamWriter writer = new StreamWriter(
                        new FileStream(txtpath, FileMode.Create)
                    )
                )
            {
                //Write some general info at the top
                writer.WriteLine(
                    string.Format(
                        "-- File: {0} | Map: {1} | GameScript: {2} --",
                        fileName, mapIndex, trigger.ScriptIndex
                    )
                );

                //Write variables - "VAR #[index]: [name]"
                for (int i = 0; i < Info.NumVars(); i++)
                {
                    writer.WriteLine(string.Format("VAR #{0}: ", i) + Info.GetVar(i));
                }
                //Write procedures - "PROC #[index]: [name]"
                for (int i = 0; i < Info.NumProcs(); i++)
                {
                    writer.WriteLine(string.Format("PROC #{0}: ", i) + Info.GetProc(i));
                }
                //Write labels - "LABEL #L[line number]: [name]"
                foreach (KeyValuePair<int, string> entry in Info.Labels)
                {
                    writer.WriteLine(string.Format("LABEL #L{0}: ", entry.Key) + entry.Value);
                }

                writer.Flush();
            }
        }
    }
}
