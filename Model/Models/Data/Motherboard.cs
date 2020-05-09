using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Motherboard
    {
        public Motherboard()
        {
            ID = 0;
            Title = null;
            Company = null;
            Series = null;
            Socket = null;
            Chipset = null;
            CPUCompany = null;
            Formfactor = null;
            MemoryType = null;
            MemorySlotsAmount = 0;
            MemoryChanelsAmount = 0;
            MaxMemory = 0;
            RAMMaxFreq = 0;
            Slots = null;
            Additions = null;
			Compatible = true;
			Incompatible = new Dictionary<string, string>();
		}

        public Motherboard(int iD, string title, string company, string series, string socket, string chipset, string cPUCompany, string formfactor, string memoryType, int memorySlotsAmount, int memoryChanelsAmount, int maxMemory, int rAMMaxFreq, IEnumerable<string> slots, string additions)
        {
            ID = iD;
            Title = title;
            Company = company;
            Series = series;
            Socket = socket;
            Chipset = chipset;
            CPUCompany = cPUCompany;
            Formfactor = formfactor;
            MemoryType = memoryType;
            MemorySlotsAmount = memorySlotsAmount;
            MemoryChanelsAmount = memoryChanelsAmount;
            MaxMemory = maxMemory;
            RAMMaxFreq = rAMMaxFreq;
            Slots = slots.ToList();
            Additions = additions;
        }

        public int							ID                  { get; set; }
	    public string						Title               { get; set; }
        public string						Company             { get; set; }
        public string						Series              { get; set; }
	    public string						Socket              { get; set; }
	    public string						Chipset             { get; set; }
        public string						CPUCompany          { get; set; }
	    public string						Formfactor          { get; set; }
        public string						MemoryType          { get; set; }
        public int							MemorySlotsAmount   { get; set; }
        public int							MemoryChanelsAmount { get; set; }
        public int							MaxMemory           { get; set; }
        public int							RAMMaxFreq          { get; set; }
        public List<string>					Slots               { get; set; }
        public string						Additions           { get; set; }
		public bool							Compatible			{ get; set; }
		public Dictionary<string, string>	Incompatible		{ get; set; }
	}
}
