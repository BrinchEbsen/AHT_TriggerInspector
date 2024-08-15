using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_Triggers.Data
{
    public class GameScriptSaveInfo
    {
        private List<string> Vars   = new List<string>();
        private List<string> Procs  = new List<string>();
        public Dictionary<int, string> Labels { get; set; }

        public GameScriptSaveInfo()
        {
            Labels = new Dictionary<int, string>();
        }

        public string GetVar(int index)
        {
            return Vars[index];
        }
        public string GetProc(int index)
        {
            return Procs[index];
        }
        public string GetLabel(int index)
        {
            return Labels[index];
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
