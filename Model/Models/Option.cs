using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Option
    {
        public string Text { get; set; }
        public bool Addition { get; set; }
        public string AdditionText { get; set; }
        public IEnumerable<string> Values { get; set; }
    }
}
