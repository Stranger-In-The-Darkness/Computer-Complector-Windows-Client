using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Data.Components
{
    [Serializable]
    public class RAM
    {
        public RAM()
        {
            ID = 0;
            Title = null;
            Company = null;
            Series = null;
            MemoryType = null;
            Purpose = null;
            Volume = 0;
            ModuleAmount = 0;
            Freq = 0;
            CL = null;
			Compatible = true;
			Incompatible = new Dictionary<string, string>();
		}

        public RAM(int iD, string title, string company, string series, string memoryType, string purpose, int volume, int moduleAmount, int freq, string cL)
        {
            ID = iD;
            Title = title;
            Company = company;
            Series = series;
            MemoryType = memoryType;
            Purpose = purpose;
            Volume = volume;
            ModuleAmount = moduleAmount;
            Freq = freq;
            CL = cL;
        }

        public int							ID				{ get; set; }
	    public string						Title			{ get; set; }
        public string						Company			{ get; set; }
	    public string						Series			{ get; set; }
	    public string						MemoryType		{ get; set; }
	    public string						Purpose			{ get; set; }
	    public int							Volume			{ get; set; }
        public int							ModuleAmount	{ get; set; }
        public int							Freq			{ get; set; }
	    public string						CL				{ get; set; }
		public bool							Compatible		{ get; set; }
		public Dictionary<string, string>	Incompatible	{ get; set; }
	}
}
