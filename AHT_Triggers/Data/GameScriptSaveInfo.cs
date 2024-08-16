using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_Triggers.Data
{
    /// <summary>
    /// Contains lists of names for variables, procedures and labels with the usual access methods for all.
    /// </summary>
    public class GameScriptSaveInfo
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly List<string> Vars = new List<string>();
        /// <summary>
        /// Procedures
        /// </summary>
        private readonly List<string> Procs = new List<string>();
        /// <summary>
        /// Labels - key is line number, value is name
        /// </summary>
        public Dictionary<int, string> Labels { get; set; }

        public GameScriptSaveInfo()
        {
            Labels = new Dictionary<int, string>();
        }

        public string GetVar(int index)
        {
            try
            {
                return Vars[index];
            } catch { return "INVALID_VAR"; }
        }
        public string GetProc(int index)
        {
            try
            {
                return Procs[index];
            } catch { return "INVALID_PROC"; }
        }
        public string GetLabel(int index)
        {
            try
            {
                return Labels[index];
            }
            catch { return "INVALID_LABEL"; }
        }

        public void SetVar(int index, string val)
        {
            Vars[index] = val;
        }
        public void SetProc(int index, string val)
        {
            Procs[index] = val;
        }
        public void SetLabel(int index, string val)
        {
            Labels[index] = val;
        }

        public void AddVar(string elem)
        {
            Vars.Add(elem);
        }
        public void AddProc(string elem)
        {
            Procs.Add(elem);
        }
        public void AddLabel(int lineNum, string elem)
        {
            Labels.Add(lineNum, elem);
        }

        public int NumVars()
        {
            return Vars.Count;
        }
        public int NumProcs()
        {
            return Procs.Count;
        }
        public int NumLabels()
        {
            return Labels.Count;
        }

        public void Clear()
        {
            Vars.Clear();
            Procs.Clear();
            Labels.Clear();
        }
    }
}
