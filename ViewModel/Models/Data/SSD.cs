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
    public class SSD : INotifyPropertyChanged
    {
        public int      ID          { get; set; }
	    public string   Title       { get; set; }
	    public string   Company     { get; set; }
	    public string   Series      { get; set; }
	    public int      Capacity    { get; set; }
	    public string   Formfactor  { get; set; }
	    public List<string>   Interface   { get; set; }
	    public string   CellType    { get; set; }

        private bool isSelected = false;
        public bool IsSelected { get => isSelected; set { isSelected = value; OnPropertyChanged("IsSelected"); } }

        public static implicit operator SSD(M.SSD b)
        {
            return b != null ? new SSD()
            {
                ID = b.ID,
                Title = b.Title,
                Company = b.Company,
                Series = b.Series,
                Capacity = b.Capacity,
                Formfactor = b.Formfactor,
                Interface = b.Interface,
                CellType = b.CellType
            } : null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override bool Equals(object obj)
        {
            SSD b = obj as SSD;
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
