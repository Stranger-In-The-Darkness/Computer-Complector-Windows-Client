using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Data.Components
{
    [Serializable]
    public class Videocard
    {
        public Videocard()
        {
            ID = 0;
            Title = null;
            Company = null;
            Series = null;
            Proccessor = null;
            VRAM = 0;
            Capacity = 0;
            Memory = null;
            Connectors = null;
            Family = null;
			Compatible = true;
			Incompatible = new Dictionary<string, string>();
		}

        public Videocard(int iD, string title, string company, string series, string proccessor, int vRAM, int capacity, string memory, IEnumerable<string> connectors, string family)
        {
            ID = iD;
            Title = title;
            Company = company;
            Series = series;
            Proccessor = proccessor;
            VRAM = vRAM;
            Capacity = capacity;
            Memory = memory;
            Connectors = connectors.ToList();
            Family = family;
        }

        public int							ID              { get; set; }
	    public string						Title           { get; set; }
        public string						Company         { get; set; }
	    public string						Series          { get; set; }
	    public string						Proccessor      { get; set; }
	    public int							VRAM            { get; set; }
	    public int							Capacity        { get; set; }
        public string						Memory          { get; set; }
	    public List<string>					Connectors      { get; set; }
        public string						Family          { get; set; }
		public bool							Compatible		{ get; set; }
		public Dictionary<string, string>	Incompatible	{ get; set; }
	}
}
