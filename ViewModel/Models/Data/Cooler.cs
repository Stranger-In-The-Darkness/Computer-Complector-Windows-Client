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
    public class Cooler : INotifyPropertyChanged
    {
        public int      ID                  { get; set; }
        public string   Title               { get; set; }
	    public string   Company             { get; set; }
	    public string   Purpose             { get; set; }
	    public string   Type                { get; set; }       
	    public List<string>   Socket              { get; set; }
	    public string   Material            { get; set; }
	    public double   VentDiam            { get; set; }
	    public bool     TurnAdj             { get; set; }
	    public string   Color               { get; set; }

        private bool isSelected = false;
        public bool IsSelected { get => isSelected; set { isSelected = value; OnPropertyChanged("IsSelected"); } }

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
                VentDiam = b.VentDiam.HasValue ? b.VentDiam.Value : 0,
                TurnAdj = b.TurnAdj.HasValue ? b.TurnAdj.Value : false
            } : null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override bool Equals(object obj)
        {
            Cooler b = obj as Cooler;
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
