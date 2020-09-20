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
    public class HDD : DataViewModelBase
    {
		private string _title;
		private string _company;
		private string _formfactor;
		private int _capacity;
		private List<string> _interface;
		private int _bufferVolume;
		private int _speed;

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
	    public int Capacity
		{
			get => _capacity;
			set
			{
				_capacity = value;

				_errors.Remove("Capacity");
				if (!ValidateIntNotLessThanValue(_capacity, 0))
				{
					_errors.Add("Capacity", "Capacity cannot be less than zero!");
				}
				OnPropertyChanged("Capacity");
				OnPropertyChanged("Error");
			}
		}
	    public List<string> Interface
		{
			get => _interface;
			set
			{
				_interface = value;

				_errors.Remove("Interface");
				foreach (string @interface in _interface)
				{
					if (!ValidateStringNotNullNotEmptyNotWhiteSpace(@interface))
					{
						_errors.Add("Interface", "Invalid data!");
					}
				}
				OnPropertyChanged("Interface");
				OnPropertyChanged("Error");
			}
		}
	    public int BufferVolume
		{
			get => _bufferVolume;
			set
			{
				_bufferVolume = value;

				_errors.Remove("BufferVolume");
				if (!ValidateIntNotLessThanValue(_bufferVolume, 0))
				{
					_errors.Add("BufferVolume", "Buffer volume cannot be less than zero!");
				}
				OnPropertyChanged("BufferVolume");
				OnPropertyChanged("Error");
			}
		}
	    public int Speed
		{
			get => _speed;
			set
			{
				_speed = value;

				_errors.Remove("Speed");
				if (!ValidateIntNotLessThanValue(_speed, 0))
				{
					_errors.Add("Speed", "Speed cannot be less than zero!");
				}
				OnPropertyChanged("Speed");
				OnPropertyChanged("Error");
			}
		}

        public static implicit operator HDD(M.HDD b)
        {
            return b != null ? new HDD()
            {
                ID = b.ID,
                Title = b.Title,
                Company = b.Company,
                Formfactor = b.Formfactor,
                Capacity = b.Capacity,
                Interface = b.Interface,
                BufferVolume = b.BufferVolume,
                Speed = b.Speed,
				Compatible = b.Compatible,
				Incompatible = b.Incompatible
            } : null;
        }

        public override bool Equals(object obj)
        {
			if (obj is HDD b)
			{
				return ID.Equals(b?.ID ?? 0) &&
					BufferVolume.Equals(b?.BufferVolume ?? 0) &&
					Capacity.Equals(b?.Capacity ?? 0) &&
					(Company?.Equals(b?.Company ?? "") ?? true) &&
					(Formfactor?.Equals(b?.Formfactor ?? "") ?? true) &&
					(Interface?.SequenceEqual(b?.Interface ?? new List<string>()) ?? true) &&
					Speed.Equals(b?.Speed ?? 0) &&
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
			return new HDD()
			{
				BufferVolume = BufferVolume,
				Capacity = Capacity,
				Company = Company,
				Formfactor = Formfactor,
				ID = ID,
				Interface = Interface?.ToList(),
				Speed = Speed,
				Title = Title,
				Compatible = Compatible,
				Incompatible = Incompatible?.ToDictionary(e => e.Key, e => e.Value),
				IsSelected = IsSelected
			};
		}
	}
}
