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
    public class Charger : DataViewModelBase, ICloneable
    { 
		private string _title;
		private string _company;
		private int _power;
		private int _videoConnectorsAmount;
		private string _connectorType;
		private int _sataAmount;
		private int _ideAmount;
		private string _motherboardConnector;

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
        public string Series { get; set; }
        public int Power
		{
			get => _power;
			set
			{
				_power = value;

				_errors.Remove("Power");
				if(!ValidateIntNotLessThanValue(_power, 1))
				{
					_errors.Add("Power", "Power cannot be less or equal to zero!");
				}
				OnPropertyChanged("Power");
				OnPropertyChanged("Error");
			}
		}
        public string Sertificate { get; set; }
        public int VideoConnectorsAmount
		{
			get => _videoConnectorsAmount;
			set
			{
				_videoConnectorsAmount = value;

				_errors.Remove("VideoConnectorsAmount");
				if (!ValidateIntNotLessThanValue(_videoConnectorsAmount, 0))
				{
					_errors.Add("VideoConnectorsAmount", "Amount of video connectors cannot be less than zero!");
				}
				OnPropertyChanged("VideoConnectorsAmount");
				OnPropertyChanged("Error");
			}
		}
        public string ConnectorType
		{
			get => _connectorType;
			set
			{
				_connectorType = value;

				_errors.Remove("ConnectorType");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_connectorType))
				{
					_errors.Add("ConnectorType", "Incorrect data!");
				}
				OnPropertyChanged("ConnectorType");
				OnPropertyChanged("Error");
			}
		}
	    public int SATAAmount
		{
			get => _sataAmount;
			set
			{
				_sataAmount = value;

				_errors.Remove("SATAAmount");
				if (!ValidateIntNotLessThanValue(_sataAmount, 0))
				{
					_errors.Add("SATAAmount", "Ampunt of SATA cannot be less than zero!");
				}

				OnPropertyChanged("SATAAmount");
				OnPropertyChanged("Error");
			}
		}
        public int IDEAmount
		{
			get => _ideAmount;
			set
			{
				_ideAmount = value;

				_errors.Remove("IDEAmount");
				if (!ValidateIntNotLessThanValue(_ideAmount, 0))
				{
					_errors.Add("IDEAmount", "Amount of IDE cannot be less than zero!");
				}
				OnPropertyChanged("IDEAmount");
				OnPropertyChanged("Error");
			}
		}
        public string MotherboardConnector
		{
			get => _motherboardConnector;
			set
			{
				_motherboardConnector = value;

				_errors.Remove("MotherboardConnector");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_motherboardConnector))
				{
					_errors.Add("MotherboardConnector", "Incorrect data!");
				}

				OnPropertyChanged("MotherboardConnector");
				OnPropertyChanged("Error");
			}
		}
	    public string Addition { get; set; }

        public static implicit operator Charger(M.Charger b)
        {
            return b != null ? new Charger()
            {
                ID = b.ID,
                Title = b.Title,
                Company = b.Company,
                Series = b.Series,
                Power = b.Power,
                Sertificate = b.Sertificate,
                VideoConnectorsAmount = b.VideoConnectorsAmount,
                ConnectorType = b.ConnectorType,
                SATAAmount = b.SATAAmount,
                IDEAmount = b.IDEAmount,
                MotherboardConnector = b.MotherboardConnector,
                Addition = b.Addition,
				Compatible = b.Compatible,
				Incompatible = b.Incompatible
            } : null;
        }

        public override bool Equals(object obj)
        {
			if (obj is Charger b)
			{
				return ID.Equals(b?.ID ?? 0) &&
					(Addition?.Equals(b.Addition ?? "") ?? true) &&
					(Company?.Equals(b?.Company ?? "") ?? true) &&
					(ConnectorType?.Equals(b?.ConnectorType ?? "") ?? true) &&
					IDEAmount.Equals(b?.IDEAmount ?? 0) &&
					(MotherboardConnector?.Equals(b?.MotherboardConnector ?? "") ?? true) &&
					Power.Equals(b?.Power ?? 0) &&
					SATAAmount.Equals(b?.SATAAmount ?? 0) &&
					(Series?.Equals(b?.Series ?? "") ?? true) &&
					(Sertificate?.Equals(b.Sertificate ?? "") ?? true) &&
					(Title?.Equals(b?.Title ?? "") ?? true) &&
					VideoConnectorsAmount.Equals(b?.VideoConnectorsAmount ?? 0);
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
			return new Charger()
			{
				ID = ID,
				Addition = Addition,
				Company = Company,
				ConnectorType = ConnectorType,
				IDEAmount = IDEAmount,
				MotherboardConnector = MotherboardConnector,
				Power = Power,
				SATAAmount = SATAAmount,
				Series = Series,
				Sertificate = Sertificate,
				Title = Title,
				VideoConnectorsAmount = VideoConnectorsAmount,
				Compatible = Compatible,
				Incompatible = Incompatible?.ToDictionary(e => e.Key, e => e.Value),
				IsSelected = IsSelected
			};
		}
	}
}
