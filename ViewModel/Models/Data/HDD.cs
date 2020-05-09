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
    public class HDD : INotifyPropertyChanged
    {
        public int      ID              { get; set; }
	    public string   Title           { get; set; }
	    public string   Company         { get; set; }
	    public string   Formfactor      { get; set; }
	    public int      Capacity        { get; set; }
	    public List<string>   Interface       { get; set; }
	    public int      BufferVolume    { get; set; }
	    public int      Speed           { get; set; }
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override bool Equals(object obj)
        {
            HDD b = obj as HDD;
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
