using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Data.Components
{
    [Serializable]
    public class Cooler
    {
        public Cooler()
        {
            ID = 0;
            Title = null;
            Company = null;
            Purpose = null;
            Type = null;
            Socket = null;
            Material = null;
            VentDiam = null;
            TurnAdj = null;
            Color = null;
			Compatible = true;
			Incompatible = new Dictionary<string, string>();
		}

        public Cooler(int iD, string title, string company, string purpose, string type, IEnumerable<string> socket, string material, double? ventDiam, bool? turnAdj, string color)
        {
            ID = iD;
            Title = title;
            Company = company;
            Purpose = purpose;
            Type = type;
            Socket = socket.ToList();
            Material = material;
            VentDiam = ventDiam;
            TurnAdj = turnAdj;
            Color = color;
        }

        public int							ID                  { get; set; }
        public string						Title               { get; set; }
	    public string						Company             { get; set; }
	    public string						Purpose             { get; set; }
	    public string						Type                { get; set; }       
	    public List<string>					Socket              { get; set; }
	    public string						Material            { get; set; }
	    public double?						VentDiam            { get; set; }
	    public bool?						TurnAdj             { get; set; }
	    public string						Color               { get; set; }
		public bool							Compatible			{ get; set; }
		public Dictionary<string, string>	Incompatible		{ get; set; }
	}
}
