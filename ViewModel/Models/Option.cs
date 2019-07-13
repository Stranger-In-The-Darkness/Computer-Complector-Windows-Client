using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using M = Model;

namespace ViewModel
{
    public class Option
    {
        public string Text { get; set; }
        public bool Addition { get; set; }
        public string AdditionText { get; set; }
        public IEnumerable<string> Values { get; set; }

        public static implicit operator Option(M.Option o)
        {
            return new Option()
            {
                Text = o.Text,
                Addition = o.Addition,
                AdditionText = o.AdditionText,
                Values = o.Values
            };
        }
    }
}
