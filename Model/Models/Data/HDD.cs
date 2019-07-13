using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class HDD
    {
        public HDD()
        {
            ID = 0;
            Title = null;
            Company = null;
            Formfactor = null;
            Capacity = 0;
            Interface = null;
            BufferVolume = 0;
            Speed = 0;
        }

        public HDD(int iD, string title, string company, string formfactor, int capacity, IEnumerable<string> @interface, int bufferVolume, int speed)
        {
            ID = iD;
            Title = title;
            Company = company;
            Formfactor = formfactor;
            Capacity = capacity;
            Interface = @interface.ToList();
            BufferVolume = bufferVolume;
            Speed = speed;
        }

        public int      ID              { get; set; }
	    public string   Title           { get; set; }
	    public string   Company         { get; set; }
	    public string   Formfactor      { get; set; }
	    public int      Capacity        { get; set; }
	    public List<string>   Interface       { get; set; }
	    public int      BufferVolume    { get; set; }
	    public int      Speed           { get; set; }
    }
}
