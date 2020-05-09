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
    public class RAM : INotifyPropertyChanged
    {
        public int      ID           { get; set; }
	    public string   Title        { get; set; }
        public string   Company      { get; set; }
	    public string   Series       { get; set; }
	    public string   MemoryType   { get; set; }
	    public string   Purpose      { get; set; }
	    public int      Volume       { get; set; }
        public int      ModuleAmount { get; set; }
        public int      Freq         { get; set; }
	    public string   CL           { get; set; }
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override bool Equals(object obj)
        {
            RAM b = obj as RAM;
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
