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
    public class Videocard : INotifyPropertyChanged
    {
        public int      ID              { get; set; }
	    public string   Title           { get; set; }
        public string   Company         { get; set; }
	    public string   Series          { get; set; }
	    public string   Proccessor      { get; set; }
	    public int      VRAM            { get; set; }
	    public int      Capacity        { get; set; }
        public string   Family          { get; set; }
	    public List<string>   Connector       { get; set; }
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

        public static implicit operator Videocard(M.Videocard b)
        {
            return b != null ? new Videocard()
            {
                ID = b.ID,
                Title = b.Title,
                Company = b.Company,
                Series = b.Series,
                Proccessor = b.Proccessor,
                VRAM = b.VRAM,
                Capacity = b.Capacity,
                Family = b.Family,
                Connector = b.Connectors,
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
            Videocard b = obj as Videocard;
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
