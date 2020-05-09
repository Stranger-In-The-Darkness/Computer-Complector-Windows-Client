using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using M = Model;

namespace ViewModel
{
    public class Charger : INotifyPropertyChanged
    { 
        public int							ID                      { get; set; }
        public string						Title                   { get; set; }
        public string						Company                 { get; set; }
        public string						Series                  { get; set; }
        public int							Power                   { get; set; }
        public string						Sertificate             { get; set; }
        public int							VideoConnectorsAmount   { get; set; }
        public string						ConnectorType           { get; set; }
	    public int							SATAAmount              { get; set; }
        public int							IDEAmount               { get; set; }
        public string						MotherboardConnector    { get; set; }
	    public string						Addition                { get; set; }
		public bool							Compatible				{ get; set; }
		public Dictionary<string, string>	Incompatible			{ get; set; }
		public int CompatibilityLevel
		{
			get
			{
				return Incompatible?.Keys.Count ?? 0;
			}
		}

		private bool isSelected = false;
        public bool IsSelected { get => isSelected; set { isSelected = value; OnPropertyChanged("IsSelected"); } }

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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override bool Equals(object obj)
        {
            Charger b = obj as Charger;
            if (b != null)
            {
                return ID == b.ID;
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
    }
}
