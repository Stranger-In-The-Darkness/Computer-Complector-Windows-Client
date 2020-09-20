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
    public class Body : DataViewModelBase, ICloneable
    {
		private string _title;
		private string _company;
		private List<string> _formfactor;
		private string _type;
		private int _chargerPower;
		private int _usb3Ports;
		private int _usb2Ports;

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
					_errors.Add("Title", "Incorrect data!");
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
					_errors.Add("Company", "Incorrect data!");
				}
				OnPropertyChanged("Company");
				OnPropertyChanged("Error");
			}
		}
        public List<string> Formfactor
		{
			get => _formfactor;
			set
			{
				_formfactor = value;
				_errors.Remove("Formfactor");
				foreach (string formfactor in _formfactor)
				{
					if (!ValidateStringNotNullNotEmptyNotWhiteSpace(formfactor))
					{
						_errors.Add("Formfactor", "Incorrect data!");
						break;
					}
				}
				OnPropertyChanged("Formfactor");
				OnPropertyChanged("Error");
			}
		}
        public string Type
		{
			get => _type;
			set
			{
				_type = value;
				_errors.Remove("Type");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_type))
				{
					_errors.Add("Type", "Incorrect data!");
				}
				OnPropertyChanged("Type");
				OnPropertyChanged("Error");
			}
		}
        public bool BuildInCharger { get; set; }
        public int ChargerPower
		{
			get => _chargerPower;
			set
			{
				_chargerPower = value;
				_errors.Remove("ChargerPower");
				if (!ValidateIntNotLessThanValue(_chargerPower, 1))
				{
					_errors.Add("ChargerPower", "Charger power cannot be less or equal to zero!");
				}
				OnPropertyChanged("ChargerPower");
				OnPropertyChanged("Error");
			}
		}
        public string Color { get; set; }
		public int USB2Ports
		{
			get => _usb2Ports;
			set
			{
				_usb2Ports = value;
				_errors.Remove("USB2Ports");
				if (!ValidateIntNotLessThanValue(_usb2Ports, 0))
				{
					_errors.Add("USB2Ports", "Amount of USB ports cannot be less than zero!");
				}
				OnPropertyChanged("USB2Ports");
				OnPropertyChanged("Error");
			}
		}
        public int USB3Ports
		{
			get => _usb3Ports;
			set
			{
				_usb3Ports = value;
				_errors.Remove("USB3Ports");
				if (!ValidateIntNotLessThanValue(_usb3Ports, 0))
				{
					_errors.Add("USB3Ports", "Amount of USB ports cannot be less than zero!");
				}
				OnPropertyChanged("USB3Ports");
				OnPropertyChanged("Error");
			}
		}
        public string Additions { get; set; }
        public string BackLightColor { get; set; }

		public static implicit operator Body(M.Body b)
        {
			return b != null ? new Body()
			{
				ID = b.ID,
				Title = b.Title,
				Company = b.Company,
				Formfactor = b.Formfactor,
				Type = b.Type,
				BuildInCharger = b.BuildInCharger,
				ChargerPower = b.ChargerPower,
				Color = b.Color,
				USB2Ports = b.USB2Ports,
				USB3Ports = b.USB3Ports,
				Additions = b.Additions,
				BackLightColor = b.BackLightColor,
				Compatible = b.Compatible,
				Incompatible = b.Incompatible
            } : null;
        }

        public override bool Equals(object obj)
        {
			if (obj is Body b)
			{
				return ID.Equals(b?.ID ?? 0) &&
					(Additions?.Equals(b?.Additions ?? "") ?? true) &&
					(BackLightColor?.Equals(b?.BackLightColor ?? "") ?? true) &&
					BuildInCharger.Equals(b?.BuildInCharger ?? false) &&
					ChargerPower.Equals(b?.ChargerPower ?? 0) &&
					(Color?.Equals(b?.Color ?? "") ?? true) &&
					(Company?.Equals(b?.Company ?? "") ?? true) &&
					(Formfactor?.SequenceEqual(b?.Formfactor ?? new List<string>()) ?? true) &&
					(Title?.Equals(b?.Title ?? "") ?? true) &&
					(Type?.Equals(b?.Type ?? "") ?? true) &&
					USB2Ports.Equals(b?.USB2Ports ?? 0) &&
					USB3Ports.Equals(b?.USB3Ports ?? 0);
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
			return new Body()
			{
				Additions = Additions,
				BackLightColor = BackLightColor,
				BuildInCharger = BuildInCharger,
				ChargerPower = ChargerPower,
				Color = Color,
				Company = Company,
				Compatible = Compatible,
				Formfactor = Formfactor?.ToList(),
				ID = ID,
				Incompatible = Incompatible?.ToDictionary(e => e.Key, e => e.Value),
				IsSelected = IsSelected,
				Title = Title,
				Type = Type,
				USB2Ports = USB2Ports,
				USB3Ports = USB3Ports,
			};
		}
	}
}
