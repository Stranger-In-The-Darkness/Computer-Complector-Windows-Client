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
    public class CPU : DataViewModelBase, ICloneable
    {
		private string _title;
		private string _company;
		private string _socket;
		private double _frequency;
		private int _coresAmount;
		private int _threadsAmount;
		private string _core;
		private string _deliveryType;

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
        public double Frequency
		{
			get => _frequency;
			set
			{
				_frequency = value;

				_errors.Remove("Frequency");
				if (!ValidateDoubleNotLessThanValue(_frequency, 0d))
				{
					_errors.Add("Frequency", "Frequency cannot be less than zero!");
				}
				OnPropertyChanged("Frequency");
				OnPropertyChanged("Error");
			}
		}
        public int CoresAmount
		{
			get => _coresAmount;
			set
			{
				_coresAmount = value;

				_errors.Remove("CoresAmount");
				if (!ValidateIntNotLessThanValue(_coresAmount, 1))
				{
					_errors.Add("CoresAmount", "Amount of cores cannot be less or equal to zero!");
				}
				OnPropertyChanged("CoresAmount");
				OnPropertyChanged("Error");
			}
		}
        public int ThreadsAmount
		{
			get => _threadsAmount;
			set
			{
				_threadsAmount = value;

				_errors.Remove("ThreadsAmount");
				if (!ValidateIntNotLessThanValue(_threadsAmount, 1))
				{
					_errors.Add("ThreadsAmount", "Amount of threads cannot be less or equal to zero!");
				}
				OnPropertyChanged("ThreadsAmount");
				OnPropertyChanged("Error");
			}
		}
        public bool IntegratedGraphics { get; set; }
        public string Core
		{
			get => _core;
			set
			{
				_core = value;

				_errors.Remove("Core");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_core))
				{
					_errors.Add("Core", "Invalid data!");
				}
				OnPropertyChanged("Core");
				OnPropertyChanged("Error");
			}
		}
        public string DeliveryType
		{
			get => _deliveryType;
			set
			{
				_deliveryType = value;

				_errors.Remove("DeliveryType");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_deliveryType))
				{
					_errors.Add("DeliveryType", "Invalid data!");
				}
				OnPropertyChanged("DeliveryType");
				OnPropertyChanged("Error");
			}
		}
        public bool Overcloacking { get; set; }

        public static implicit operator CPU(M.CPU b)
        {
            return b != null ? new CPU()
            {
                ID = b.ID,
                Title = b.Title,
                Company = b.Company,
                Series = b.Series,
                Socket = b.Socket,
                Frequency = b.Frequency,
                CoresAmount = b.CoresAmount,
                ThreadsAmount = b.ThreadsAmount,
                IntegratedGraphics = b.IntegratedGraphics,
                Core = b.Core,
                DeliveryType = b.DeliveryType,
                Overcloacking = b.Overcloacking,
				Compatible = b.Compatible,
				Incompatible = b.Incompatible
            } : null;
        }

        public override bool Equals(object obj)
        {
			if (obj is CPU b)
			{
				return ID.Equals(b?.ID ?? 0) &&
					(Company?.Equals(b?.Company ?? "") ?? true) &&
					(Core?.Equals(b?.Core ?? "") ?? true) &&
					CoresAmount.Equals(b?.CoresAmount ?? 0) &&
					(DeliveryType?.Equals(b?.DeliveryType ?? "") ?? true) &&
					Frequency.Equals(b?.Frequency ?? 0.0d) &&
					IntegratedGraphics.Equals(b?.IntegratedGraphics ?? false) &&
					Overcloacking.Equals(b?.Overcloacking ?? false) &&
					(Series?.Equals(b?.Series ?? "") ?? true) &&
					(Socket?.Equals(b?.Socket ?? "") ?? true) &&
					ThreadsAmount.Equals(b?.ThreadsAmount ?? 0) &&
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
			return new CPU()
			{
				Company = Company,
				Core = Core,
				CoresAmount = CoresAmount,
				DeliveryType = DeliveryType,
				Frequency = Frequency,
				ID = ID,
				IntegratedGraphics = IntegratedGraphics,
				Overcloacking = Overcloacking,
				Series = Series,
				Socket = Socket,
				ThreadsAmount = ThreadsAmount,
				Title = Title,
				Compatible = Compatible,
				Incompatible = Incompatible?.ToDictionary(e => e.Key, e => e.Value),
				IsSelected = IsSelected
			};
		}
	}
}
