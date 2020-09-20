using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Events.Arguments
{
	public class BeginEditingModelArgs
	{
		public string ModelType { get; set; }
		public object Model { get; set; }
	}
}
