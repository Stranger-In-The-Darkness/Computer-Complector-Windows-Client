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
    public class RAM : DataViewModelBase
    {
		private string _title;
		private string _company;
		private string _memoryType;
		private string _purpose;
		private int _volume;
		private int _modulesAmount;
		private int _frequency;
		private string _casLatency;

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
	    public string MemoryType
		{
			get => _memoryType;
			set
			{
				_memoryType = value;

				_errors.Remove("MemoryType");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_memoryType))
				{
					_errors.Add("MemoryType", "Invalid data!");
				}
				OnPropertyChanged("MemoryType");
				OnPropertyChanged("Error");
			}
		}
	    public string Purpose
		{
			get => _purpose;
			set
			{
				_purpose = value;

				_errors.Remove("Purpose");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_purpose))
				{
					_errors.Add("Purpose", "Invalid data!");
				}
				OnPropertyChanged("Purpose");
				OnPropertyChanged("Error");
			}
		}
	    public int Volume
		{
			get => _volume;
			set
			{
				_volume = value;

				_errors.Remove("Volume");
				if (!ValidateIntNotLessThanValue(_volume, 1))
				{
					_errors.Add("Volume", "Volume cannot be less or equal to zero!");
				}
				OnPropertyChanged("Volume");
				OnPropertyChanged("Error");
			}
		}
        public int ModuleAmount
		{
			get => _modulesAmount;
			set
			{
				_modulesAmount = value;

				_errors.Remove("ModuleAmount");
				if (!ValidateIntNotLessThanValue(_modulesAmount, 1))
				{
					_errors.Add("ModuleAmount", "Amount of modules cannot be less or equal to zero!");
				}
				OnPropertyChanged("ModuleAmount");
				OnPropertyChanged("Error");
			}
		}
        public int Freq
		{
			get => _frequency;
			set
			{
				_frequency = value;

				_errors.Remove("Freq");
				if (!ValidateIntNotLessThanValue(_frequency, 1))
				{
					_errors.Add("Freq", "Frequency cannot be less or equal to zero!");
				}
				OnPropertyChanged("Freq");
				OnPropertyChanged("Error");
			}
		}
	    public string CL
		{
			get => _casLatency;
			set
			{
				_casLatency = value;

				_errors.Remove("CL");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_casLatency))
				{
					_errors.Add("CL", "Invalid data!");
				}
				OnPropertyChanged("CL");
				OnPropertyChanged("Error");
			}
		}

        public static implicit operator RAM(M.RAM b)
        {
            return b != null ? new RAM()
            {
                ID = b.ID,
                Title = b.Title,
                Company = b.Company,
                Series = b.Series,
                MemoryType = b.MemoryType,
                Purpose = b.Purpose,
                Volume = b.Volume,
                ModuleAmount = b.ModuleAmount,
                Freq = b.Freq,
                CL = b.CL,
				Compatible = b.Compatible,
				Incompatible = b.Incompatible
            } : null;
        }

        public override bool Equals(object obj)
        {
			if (obj is RAM b)
			{
				return ID.Equals(b?.ID ?? 0) &&
					(CL?.Equals(b?.CL ?? "") ?? true) &&
					(Company?.Equals(b?.Company ?? "") ?? true) &&
					Freq.Equals(b?.Freq ?? 0) &&
					(MemoryType?.Equals(b?.MemoryType ?? "") ?? true) &&
					ModuleAmount.Equals(b?.ModuleAmount ?? 0) &&
					(Purpose?.Equals(b?.Purpose ?? "") ??  true) &&
					(Series?.Equals(b?.Series ?? "") ?? true) &&
					(Title?.Equals(b?.Title ?? "") ?? true) &&
					Volume.Equals(b?.Volume ?? 0);
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
			return new RAM()
			{
				CL = CL,
				Company = Company,
				Freq = Freq,
				ID = ID,
				MemoryType = MemoryType,
				ModuleAmount = ModuleAmount,
				Purpose = Purpose,
				Series = Series,
				Title = Title,
				Volume = Volume,
				Compatible = Compatible,
				Incompatible = Incompatible?.ToDictionary(e => e.Key, e => e.Value),
				IsSelected = IsSelected
			};
		}
	}
}
