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
    public class Motherboard : INotifyPropertyChanged
    {
        public int      ID                  { get; set; }
	    public string   Title               { get; set; }
        public string   Company             { get; set; }
        public string   Series              { get; set; }
	    public string   Socket              { get; set; }
	    public string   Chipset             { get; set; }
        public string   CPUCompany          { get; set; }
	    public string   Formfactor          { get; set; }
        public string   Memory              { get; set; }
        public int      MemorySlotsAmount   { get; set; }
        public int      MemoryChanelsAmount { get; set; }
        public int      MaxMemory           { get; set; }
        public int      RAMMaxFreq          { get; set; }
        public List<string>    Slots         { get; set; }
        public string   Additions           { get; set; }
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override bool Equals(object obj)
        {
            Motherboard b = obj as Motherboard;
            if ( b != null)
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
