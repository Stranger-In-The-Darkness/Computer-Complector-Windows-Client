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
    public class Motherboard : DataViewModelBase
    {
		private string _title;
		private string _company;
		private string _socket;
		private string _chipset;
		private string _formfactor;
		private string _memory;
		private int _memorySlotsAmount;
		private int _memoryChanelsAmount;
		private int _maxMemory;
		private int _ramMaxFreq;
		private List<string> _slots;

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
	    public string Socket
		{
			get => _socket;
			set
			{
				_socket = value;

				_errors.Remove("Socket");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_socket))
				{
					_errors.Add("Socket", "Invalid data!");
				}
				OnPropertyChanged("Socket");
				OnPropertyChanged("Error");
			}
		}
	    public string Chipset
		{
			get => _chipset;
			set
			{
				_chipset = value;

				_errors.Remove("Chipset");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_chipset))
				{
					_errors.Add("Chipset", "Invalid data!");
				}
				OnPropertyChanged("Chipset");
				OnPropertyChanged("Error");
			}
		}
        public string CPUCompany { get; set; }
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
        public string Memory
		{
			get => _memory;
			set
			{
				_memory = value;

				_errors.Remove("Memory");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_memory))
				{
					_errors.Add("Memory", "Invalid data!");
				}
				OnPropertyChanged("Memory");
				OnPropertyChanged("Error");
			}
		}
        public int MemorySlotsAmount
		{
			get => _memorySlotsAmount;
			set
			{
				_memorySlotsAmount = value;

				_errors.Remove("MemorySlotsAmount");
				if (!ValidateIntNotLessThanValue(_memorySlotsAmount, 1))
				{
					_errors.Add("MemorySlotsAmount", "Amount of memory slots cannot be less or equal to zero!");
				}
				OnPropertyChanged("MemorySlotsAmount");
				OnPropertyChanged("Error");
			}
		}
        public int MemoryChanelsAmount
		{
			get => _memoryChanelsAmount;
			set
			{
				_memoryChanelsAmount = value;

				_errors.Remove("MemoryChanelsAmount");
				if (!ValidateIntNotLessThanValue(_memoryChanelsAmount, 1))
				{
					_errors.Add("MemoryChanelsAmount", "Amount of memory chanels cannot be less or equal to zero!");
				}
				OnPropertyChanged("MemoryChanelsAmount");
				OnPropertyChanged("Error");
			}
		}
        public int MaxMemory
		{
			get => _maxMemory;
			set
			{
				_maxMemory = value;

				_errors.Remove("MaxMemory");
				if (!ValidateIntNotLessThanValue(_maxMemory, 1))
				{
					_errors.Add("MaxMemory", "Maximum memory cannot be less or equal to zero!");
				}
				OnPropertyChanged("MaxMemory");
				OnPropertyChanged("Error");
			}
		}
        public int RAMMaxFreq
		{
			get => _ramMaxFreq;
			set
			{
				_ramMaxFreq = value;

				_errors.Remove("RAMMaxFreq");
				if (!ValidateIntNotLessThanValue(_ramMaxFreq, 1))
				{
					_errors.Add("RAMMaxFreq", "Maximum RAM frequency cannot be less or equal to zero!");
				}
				OnPropertyChanged("RAMMaxFreq");
				OnPropertyChanged("Error");
			}
		}
        public List<string> Slots
		{
			get => _slots;
			set
			{
				_slots = value;

				_errors.Remove("Slots");
				foreach(string slot in _slots)
				{
					if (!ValidateStringNotNullNotEmptyNotWhiteSpace(slot))
					{
						_errors.Add("Slots", "Invalid data!");
						break;
					}
				}
				OnPropertyChanged("Slots");
				OnPropertyChanged("Error");
			}
		}
        public string Additions { get; set; }

        public static implicit operator Motherboard(M.Motherboard b)
        {
            return b != null ? new Motherboard()
            {
                ID = b.ID,
                Title = b.Title,
                Company = b.Company,
                Series = b.Series,
                Socket = b.Socket,
                Chipset = b.Chipset,
                CPUCompany = b.CPUCompany,
                Formfactor = b.Formfactor,
                Memory = b.MemoryType,
                MaxMemory = b.MaxMemory,
                MemoryChanelsAmount = b.MemoryChanelsAmount,
                MemorySlotsAmount = b.MemorySlotsAmount,
                RAMMaxFreq = b.RAMMaxFreq,
                Slots = b.Slots,
                Additions = b.Additions,
				Compatible = b.Compatible,
				Incompatible = b.Incompatible
            } : null;
        }

        public override bool Equals(object obj)
        {
			if (obj is Motherboard b)
			{
				return ID.Equals(b?.ID ?? 0) &&
					(Additions?.Equals(b.Additions ?? "") ?? true) &&
					(Chipset?.Equals(b?.Chipset ?? "") ?? true) &&
					(Company?.Equals(b?.Company ?? "") ?? true) &&
					(CPUCompany?.Equals(b.CPUCompany ?? "") ?? true) &&
					(Formfactor?.Equals(b?.Formfactor ?? "") ?? true) &&
					ID.Equals(b?.ID ?? 0) &&
					MaxMemory.Equals(b?.MaxMemory ?? 0) &&
					(Memory?.Equals(b?.Memory ?? "") ?? true) &&
					MemoryChanelsAmount.Equals(b?.MemoryChanelsAmount ?? 0) &&
					MemorySlotsAmount.Equals(b?.MemorySlotsAmount ?? 0) &&
					RAMMaxFreq.Equals(b?.RAMMaxFreq ?? 0) &&
					(Series?.Equals(b?.Series ?? "") ?? true) &&
					(Slots?.SequenceEqual(b?.Slots ?? new List<string>()) ?? true) &&
					(Socket?.Equals(b?.Socket ?? "") ?? true) &&
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
			return new Motherboard()
			{
				Additions = Additions,
				Chipset = Chipset,
				Company = Company,
				CPUCompany = CPUCompany,
				Formfactor = Formfactor,
				ID = ID,
				MaxMemory = MaxMemory,
				Memory = Memory,
				MemoryChanelsAmount = MemoryChanelsAmount,
				MemorySlotsAmount = MemorySlotsAmount,
				RAMMaxFreq = RAMMaxFreq,
				Series = Series,
				Slots = Slots?.ToList(),
				Socket = Socket,
				Title = Title,
				Compatible = Compatible,
				Incompatible = Incompatible?.ToDictionary(e => e.Key, e => e.Value),
				IsSelected = IsSelected
			};
		}
	}
}
