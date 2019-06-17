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
    public class Body : INotifyPropertyChanged
    {
        public int      ID                  { get; set; }
        public string   Title               { get; set; }
        public string   Company             { get; set; }
        public string   Formfactor          { get; set; }
        public string   Type                { get; set; }
        public bool     BuildInCharger      { get; set; }
        public int      ChargerPower        { get; set; }
        public string   Color               { get; set; }
        public int      USB3Ports           { get; set; }
		public int      USB2Ports           { get; set; }
        public string   Additions           { get; set; }
        public string   BackLightColor      { get; set; }

        private bool    isSelected = false;
        public bool     IsSelected          { get => isSelected; set { isSelected = value; OnPropertyChanged("IsSelected"); } }

        public static implicit operator Body(M.Body b)
        {
            return b != null ? new Body()
            {
                ID = b.ID,
                Title = b.Title,
                Company = b.Company,
                Formfactor = b.Formfactor,
                Type = b.Type,
                BuildInCharger = b.BuildInCharger,
                ChargerPower = b.ChargerPower,
                Color = b.Color,
                USB2Ports = b.USB2Ports,
                USB3Ports = b.USB3Ports,
                Additions = b.Additions,
                BackLightColor = b.BackLightColor
            } : null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override bool Equals(object obj)
        {
            Body b = obj as Body;
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
