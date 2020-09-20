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
    public class Cooler : DataViewModelBase, ICloneable
    {
		private string _title;
		private string _company;
		private string _purpose;
		private string _type;
		private List<string> _socket;
		private double _ventDiam;
		private string _material;

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
		public string Purpose
		{
			get => _purpose;
			set
			{
				_purpose = value;

				_errors.Remove("Purpose");

				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_purpose))
				{
					_errors.Add("Purpose", "Incorrect data!");
				}

				OnPropertyChanged("Purpose");
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
	    public List<string> Socket
		{
			get => _socket;
			set
			{
				_socket = value;

				_errors.Remove("Socket");
				foreach(string socket in _socket)
				{
					if (!ValidateStringNotNullNotEmptyNotWhiteSpace(socket))
					{
						_errors.Add("Socket", "Incorrect data!");
						break;
					}
				}
				OnPropertyChanged("Socket");
				OnPropertyChanged("Error");
			}
		}
	    public string Material
		{
			get => _material;
			set
			{
				_material = value;

				_errors.Remove("Material");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_material))
				{
					_errors.Add("Material", "Incorrect data!");
				}

				OnPropertyChanged("Material");
				OnPropertyChanged("Error");
			}
		}
	    public double VentDiam
		{
			get => _ventDiam;
			set
			{
				_ventDiam = value;

				_errors.Remove("VentDiam");
				if (!ValidateDoubleNotLessThanValue(_ventDiam, 0d))
				{
					_errors.Add("VentDiam", "Ventilator diameter cannot be less than zero!");
				}
				OnPropertyChanged("VentDiam");
				OnPropertyChanged("Error");
			}
		}
	    public bool TurnAdj { get; set; }
	    public string Color { get; set; }

        public static implicit operator Cooler(M.Cooler b)
        {
            return b != null ? new Cooler()
            {
                ID = b.ID,
                Title = b.Title,
                Company = b.Company,
                Purpose = b.Purpose,
                Type = b.Type,
                Socket = b.Socket,
                Material = b.Material,
                Color = b.Color,
                VentDiam = b.VentDiam ?? 0,
                TurnAdj = b.TurnAdj ?? false,
				Compatible = b.Compatible,
				Incompatible = b.Incompatible
            } : null;
        }

        public override bool Equals(object obj)
        {
			if (obj is Cooler b)
			{
				return ID.Equals(b?.ID ?? 0) &&
					(Color?.Equals(b.Color ?? "") ?? true) &&
					(Company?.Equals(b?.Company ?? "") ?? true) &&
					(Material?.Equals(b?.Material ?? "") ?? true) &&
					(Purpose?.Equals(b?.Purpose ?? "") ?? true) &&
					(Socket?.SequenceEqual(b?.Socket ?? new List<string>()) ?? true) &&
					(Title?.Equals(b?.Title ?? "") ?? true) &&
					TurnAdj.Equals(b?.TurnAdj ?? false) &&
					(Type?.Equals(b?.Type ?? "") ?? true) &&
					VentDiam.Equals(b?.VentDiam ?? 0.0d);
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
			return new Cooler()
			{
				Color = Color,
				Company = Company,
				ID = ID,
				Material = Material,
				Purpose = Purpose,
				Socket = Socket?.ToList(),
				Title = Title,
				TurnAdj = TurnAdj,
				Type = Type,
				VentDiam = VentDiam,
				Compatible = Compatible,
				Incompatible = Incompatible?.ToDictionary(e => e.Key, e => e.Value),
				IsSelected = IsSelected
			};
		}
	}
}
