using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Models
{
	public abstract class DataViewModelBase : ViewModelBase
	{
		protected bool _isSelected;

		public virtual bool Compatible { get; set; }
		public virtual Dictionary<string, string> Incompatible { get; set; }
		public virtual int CompatibilityLevel
		{
			get
			{
				return Incompatible?.Keys.Count ?? 0;
			}
		}
		public virtual bool IsSelected
		{
			get => _isSelected;
			set
			{
				_isSelected = value;
				OnPropertyChanged("IsSelected");
			}
		}
	}
}
