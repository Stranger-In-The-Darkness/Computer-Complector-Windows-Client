using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Delegates;

namespace ViewModel.Interfaces
{
	public interface IModelEditor
	{
		RelayCommand ApplyChanges { get; }

		RelayCommand DeclineChanges { get; }

		RelayCommand CloseModelEdit { get; }

		event UnsavedChangesClose OnUnsavedChangesClose;

		event ModelEditClosed OnModelEditClosed;
	}
}
