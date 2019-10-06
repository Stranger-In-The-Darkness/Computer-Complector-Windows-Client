using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// Search filter description class
	/// </summary>
    public class Option
    {
		/// <summary>
		/// Filter name
		/// </summary>
        public string Text { get; set; }
		/// <summary>
		/// Defines whether filter has a description
		/// </summary>
        public bool Addition { get; set; }
		/// <summary>
		/// Filter description
		/// </summary>
        public string AdditionText { get; set; }
		/// <summary>
		/// Available values
		/// </summary>
        public IEnumerable<string> Values { get; set; }
    }
}
