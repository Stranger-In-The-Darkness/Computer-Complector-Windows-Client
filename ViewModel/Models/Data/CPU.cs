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
    public class CPU : INotifyPropertyChanged
    {
        public int     ID                  { get; set; }
        public string  Title               { get; set; }
        public string  Company             { get; set; }
        public string  Series              { get; set; }
        public string  Socket              { get; set; }
        public double  Frequency           { get; set; }
        public int     CoresAmount         { get; set; }
        public int     ThreadsAmount       { get; set; }
        public bool    IntegratedGraphics  { get; set; }
        public string  Core                { get; set; }
        public string  DeliveryType        { get; set; }
        public bool    Overcloacking       { get; set; }
		public bool Compatible { get; set; }
		public Dictionary<string, string> Incompatible { get; set; }
		public int CompatibilityLevel
		{
			get
			{
				return Incompatible?.Keys.Count ?? 0;
			}
		}

		private bool isSelected = false;
        public bool IsSelected { get => isSelected; set { isSelected = value; OnPropertyChanged("IsSelected"); } }

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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override bool Equals(object obj)
        {
            CPU b = obj as CPU;
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
