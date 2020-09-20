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
    public class Videocard : DataViewModelBase, ICloneable
    {
		private string _title;
		private string _company;
		private string _proccessor;
		private int _vram;
		private int _capacity;
		private string _family;
		private List<string> _connector;

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
	    public string Proccessor
		{
			get => _proccessor;
			set
			{
				_proccessor = value;

				_errors.Remove("Proccessor");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_proccessor))
				{
					_errors.Add("Proccessor", "Invalid data");
				}
				OnPropertyChanged("Proccessor");
				OnPropertyChanged("Error");
			}
		}
	    public int VRAM
		{
			get => _vram;
			set
			{
				_vram = value;

				_errors.Remove("VRAM");
				if (!ValidateIntNotLessThanValue(_vram, 1))
				{
					_errors.Add("VRAM", "VRAM cannot be less or equal to zero!");
				}
				OnPropertyChanged("VRAM");
				OnPropertyChanged("Error");
			}
		}
	    public int Capacity
		{
			get => _capacity;
			set
			{
				_capacity = value;

				_errors.Remove("Capacity");
				if (!ValidateIntNotLessThanValue(_capacity, 1))
				{
					_errors.Add("Capacity", "Capacity cannot be less or equal to zero!");
				}
				OnPropertyChanged("Capacity");
				OnPropertyChanged("Error");
			}
		}
        public string Family
		{
			get => _family;
			set
			{
				_family = value;

				_errors.Remove("Family");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_family))
				{
					_errors.Add("Family", "Invalid data");
				}
				OnPropertyChanged("Family");
				OnPropertyChanged("Error");
			}
		}
	    public List<string> Connector
		{
			get => _connector;
			set
			{
				_connector = value;
				_errors.Remove("Connector");
				foreach(string connector in _connector)
				{
					if (!ValidateStringNotNullNotEmptyNotWhiteSpace(connector))
					{
						_errors.Add("Connector", "Invalid data!");
						break;
					}
				}
				OnPropertyChanged("Connector");
				OnPropertyChanged("Error");
			}
		}

        public static implicit operator Videocard(M.Videocard b)
        {
            return b != null ? new Videocard()
            {
                ID = b.ID,
                Title = b.Title,
                Company = b.Company,
                Series = b.Series,
                Proccessor = b.Proccessor,
                VRAM = b.VRAM,
                Capacity = b.Capacity,
                Family = b.Family,
                Connector = b.Connectors,
				Compatible = b.Compatible,
				Incompatible = b.Incompatible
            } : null;
        }

        public override bool Equals(object obj)
        {
			if (obj is Videocard b)
			{
				return ID.Equals(b?.ID ?? 0) &&
					Capacity.Equals(b?.Capacity ?? 0) &&
					(Company?.Equals(b?.Company ?? "") ?? true) &&
					(Connector?.SequenceEqual(b?.Connector ?? new List<string>()) ?? true) &&
					(Family?.Equals(b?.Family ?? "") ?? true) &&
					(Proccessor?.Equals(b?.Proccessor ?? "") ?? true) &&
					(Series?.Equals(b?.Series ?? "") ?? true) &&
					(Title?.Equals(b?.Title ?? "") ?? true) &&
					VRAM.Equals(b?.VRAM ?? 0);
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
			return new Videocard()
			{
				Capacity = Capacity,
				Company = Company,
				Connector = Connector?.ToList(),
				Family = Family,
				ID = ID,
				Proccessor = Proccessor,
				Series = Series,
				Title = Title,
				VRAM = VRAM,
				Compatible = Compatible,
				Incompatible = Incompatible?.ToDictionary(e => e.Key, e => e.Value),
				IsSelected = IsSelected
			};
		}
	}
}
