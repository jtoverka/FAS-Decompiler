using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAS_Decompiler
{
    public class FasCommando
    {
        public long Position { get; set; }
        public long Commando { get; set; }
        public List<int> Parameters { get; set; }
        public string Disassembled { get; set; }
    }
}
