using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;
using M = Model.Models.Data.Components;

namespace ViewModel
{
    public class SSD : DataViewModelBase
    {
		private string _title;
		private string _company;
		private int _capacity;
		private string _formfactor;
		private List<string> _interface;
		private string _cellType;

        public int ID { get; set; }
	    public string Title
		{
			get => _title;
			set
			{
				_title = value;

				_errors.Remove("Title");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_title))
				{
					_errors.Add("Title", "Invalid data!");
				}
				OnPropertyChanged("Title");
				OnPropertyChanged("Error");
			}
		}
	    public string Company
		{
			get => _company;
			set
			{
				_company = value;

				_errors.Remove("Company");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_company))
				{
					_errors.Add("Company", "Invalid data!");
				}
				OnPropertyChanged("Company");
				OnPropertyChanged("Error");
			}
		}
	    public string Series { get; set; }
	    public int Capacity
		{
			get => _capacity;
			set
			{
				_capacity = value;

				_errors.Remove("Capacity");
				if(!ValidateIntNotLessThanValue(_capacity, 1))
				{
					_errors.Add("Capacity", "Capacity cannot be less or equal to zero!");
				}
				OnPropertyChanged("Capacity");
				OnPropertyChanged("Error");
			}
		}
	    public string Formfactor
		{
			get => _formfactor;
			set
			{
				_formfactor = value;

				_errors.Remove("Formfactor");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_formfactor))
				{
					_errors.Add("Formfactor", "Invalid data!");
				}
				OnPropertyChanged("Formfactor");
				OnPropertyChanged("Error");
			}
		}
	    public List<string> Interface
		{
			get => _interface;
			set
			{
				_interface = Interface;

				_errors.Remove("Interfaace");
				foreach(string @interface in _interface)
				{
					if (!ValidateStringNotNullNotEmptyNotWhiteSpace(@interface))
					{
						_errors.Add("Interfaace", "Invalid data!");
						break;
					}
				}
				OnPropertyChanged("Interface");
				OnPropertyChanged("Error");
			}
		}
	    public string CellType
		{
			get => _cellType;
			set
			{
				_cellType = value;

				_errors.Remove("CellType");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_cellType))
				{
					_errors.Add("CellType", "Invalid data!");
				}
				OnPropertyChanged("CellType");
				OnPropertyChanged("Error");
			}
		}

        public static implicit operator SSD(M.SSD b)
        {
            return b != null ? new SSD()
            {
                ID = b.ID,
                Title = b.Title,
                Company = b.Company,
                Series = b.Series,
                Capacity = b.Capacity,
                Formfactor = b.Formfactor,
                Interface = b.Interface,
                CellType = b.CellType,
				Compatible = b.Compatible,
				Incompatible = b.Incompatible
            } : null;
        }

        public override bool Equals(object obj)
        {
			if (obj is SSD b)
			{
				return ID.Equals(b?.ID ?? 0) &&
					Capacity.Equals(b?.Capacity ?? 0) &&
					(CellType?.Equals(b?.CellType ?? "") ?? true) &&
					(Company?.Equals(b?.Company ?? "") ?? true) &&
					(Formfactor?.Equals(b?.Formfactor ?? "") ?? true) &&
					(Interface?.SequenceEqual(b?.Interface ?? new List<string>()) ?? true) &&
					(Series?.Equals(b?.Series ?? "") ?? true) &&
					(Title?.Equals(b?.Title ?? "") ?? true);
			}
			else
			{
				return false;
			}
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

		public override object Clone()
		{
			return new SSD()
			{
				Capacity = Capacity,
				CellType = CellType,
				Company = Company,
				Formfactor = Formfactor,
				ID = ID,
				Interface = Interface?.ToList(),
				Series = Series,
				Title = Title,
				Compatible = Compatible,
				Incompatible = Incompatible?.ToDictionary(e => e.Key, e => e.Value),
				IsSelected = IsSelected
			};
		}
	}
}
