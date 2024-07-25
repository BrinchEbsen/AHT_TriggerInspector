using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AHT_Triggers.Data
{
    internal class ByteCodeDecompiler
    {
        private int Indentation = 0;

        public ByteCodeDecompiler() { }

        private void IncIndentation()
        {
            Indentation++;
        }

        private void DecIndentation()
        {
            Indentation--;
            if (Indentation < 0 )
            {
                Indentation = 0;
                Console.WriteLine("WARNING: Indentation became negative!");
            }
        }

        public string ScriptToString(GameScript script)
        {
            return "TEMP";
        }

        public string DecipherCommand(CodeLine line)
        {
            return "TEMP";
        }
    }
}
