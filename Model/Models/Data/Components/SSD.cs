using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Data.Components
{
    [Serializable]
    public class SSD
    {
        public SSD()
        {
            ID = 0;
            Title = null;
            Company = null;
            Series = null;
            Capacity = 0;
            Formfactor = null;
            Interface = null;
            CellType = null;
			Compatible = true;
			Incompatible = new Dictionary<string, string>();
		}

        public SSD(int iD, string title, string company, string series, int capacity, string formfactor, IEnumerable<string> @interface, string cellType)
        {
            ID = iD;
            Title = title;
            Company = company;
            Series = series;
            Capacity = capacity;
            Formfactor = formfactor;
            Interface = @interface.ToList();
            CellType = cellType;
        }

        public int							ID				{ get; set; }
	    public string						Title			{ get; set; }
	    public string						Company			{ get; set; }
	    public string						Series			{ get; set; }
	    public int							Capacity		{ get; set; }
	    public string						Formfactor		{ get; set; }
	    public List<string>					Interface		{ get; set; }
	    public string						CellType		{ get; set; }
		public bool							Compatible		{ get; set; }
		public Dictionary<string, string>	Incompatible	{ get; set; }
	}
}
