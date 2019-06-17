using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

using Model;

namespace ViewModel
{
    public class ViewModelMock : INotifyPropertyChanged
    {
        private Model.Model _model = new Model.Model("");

        private int _selectedIndex = 3;
        public int SelectedTab
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged("SelectedTab");
            }
        }

        private RelayCommand changeTab;
        public RelayCommand ChangeTab
        {
            get
            {
                return changeTab ?? (changeTab = new RelayCommand(obj => { int selected = int.Parse(obj.ToString()); SelectedTab = selected; }, (obj) => { return int.Parse(obj.ToString()) >= 0 && int.Parse(obj.ToString()) < 9; }));
            }
        }

        private RelayCommand selectFilter;
        public RelayCommand SelectFilter
        {
            get
            {
                return selectFilter ??
                    (selectFilter = new RelayCommand(
                        (obj) =>
                        {
                            var values = (Tuple<string, int>)obj;

                            _selectedFields[_selectedIndex][values.Item1] = values.Item2;
                        },
                        (obj) =>
                        {
                            return true;
                        }));
            }
        }

        public ObservableCollection<Body> Bodies { get; set; } = new ObservableCollection<Body>();
        public Dictionary<string, Tuple<bool, string, IEnumerable<string>>> BODYFields { get; set; } = new Dictionary<string, Tuple<bool, string, IEnumerable<string>>>()
        {
            { "Company", new Tuple<bool, string, IEnumerable<string>>(false, "", new string[] { "BODYCompany1", "BODYCompany2" }) },
            { "Formfactor", new Tuple<bool, string, IEnumerable<string>>(true, "Determines form of the body", new string[] { "BODYFormfactor1", "BODYFormfactor2" }) },
            { "Type", new Tuple<bool, string, IEnumerable<string>>(false, "", new string[] { "BODYType1", "BODYType2" }) },
            { "USB ports", new Tuple<bool, string, IEnumerable<string>>(false, "", new string[] { "1", "2", "3", "4", "5" }) },
            { "Charger power", new Tuple<bool, string, IEnumerable<string>>(false, "", new string[] { "60-120", "150-270", "300-350" }) },
            { "Build-in charger", new Tuple<bool, string, IEnumerable<string>>(false, "", new string[] { "Yes", "No" }) },
            { "Color", new Tuple<bool, string, IEnumerable<string>>(false, "", new string[] { "#000000", "#FFFFFF" }) },
            { "Backlight color", new Tuple<bool, string, IEnumerable<string>>(false, "", new string[] { "#fffevd", "#fgghty" }) }
        };
        public ObservableCollection<Charger> Chargers { get; set; } = new ObservableCollection<Charger>()
        {

        };
        public Dictionary<string, IEnumerable<string>> CHARGERFields { get; set; }
        public ObservableCollection<Cooler> Coolers { get; set; } = new ObservableCollection<Cooler>()
        {

        };
        public Dictionary<string, IEnumerable<string>> COOLERFields { get; set; }
        public ObservableCollection<CPU> CPUs { get; set; } = new ObservableCollection<CPU>()
        {
            new CPU()
            {
                Title = "CPU1",
                Company = "CPUCompany1",
                Core = "Core1",
                CoresAmount = 4,
                DeliveryType = "Box",
                ID = 0,
                IntegratedGraphics = false,
                Overcloacking = true,
                Series = "CPUSeries1",
                Socket = "CPUSocket1",
                ThreadsAmount = 16,
                Frequency = 3.4
            },
            new CPU()
            {
                Title = "CPU2",
                Company = "CPUCompany1",
                Core = "Core3",
                CoresAmount = 4,
                DeliveryType = "Box",
                ID = 0,
                IntegratedGraphics = false,
                Overcloacking = true,
                Series = "CPUSeries2",
                Socket = "CPUSocket1",
                ThreadsAmount = 16,
                Frequency = 3.4
            }
        };
        public Dictionary<string, IEnumerable<string>> CPUFields { get; set; } = new Dictionary<string, IEnumerable<string>>()
        {
            {
                "Company", new string[] { "--Not selected--", "Intel", "AMD" }
            },
            {
                "Core", new string[] { "--Not selected--", "Core1", "Core2", "Core3" }
            },
            {
                "Cores amount", new string[] { "--Not selected--", "2", "4", "8" }
            },
            {
                "Delivery type", new string[] { "--Not selected--", "Box", "Tray" }
            },
            {
                "Integrated graphics", new string[] { "--Not selected--", "Yes", "No" }
            },
            {
                "Overcloacking", new string[] { "--Not selected--", "Yes", "No" }
            },
            {
                "Series", new string[] { "--Not selected--", "Core i3", "Core i5", "Core i7", "Radeon" }
            },
            {
                "Socket", new string[] {"--Not selected", "AM4", "DM6" }
            },
            {
                "Threads amount", new string[] {"2","4","8","16"}
            },
            {
                "Frequency", new string[] { "3.2", "3.4", "3.6" }
            }
        };
        public ObservableCollection<HDD> HDDs { get; set; } = new ObservableCollection<HDD>()
        {

        };
        public Dictionary<string, IEnumerable<string>> HDDFields { get; set; }
        public ObservableCollection<Motherboard> Motherboards { get; set; } = new ObservableCollection<Motherboard>();
        public Dictionary<string, IEnumerable<string>> MOTHERBOARDFields { get; set; } = new Dictionary<string, IEnumerable<string>>()
        {
            { "Company", new string[] { "--Not selected--", "MOTHERBOARDCompany1", "MOTHERBOARDCompany2", "MOTHERBOARDCompany3" } },
            { "Series", new string[] { "--Not selected--", "MOTHERBOARDSeries1", "MOTHERBOARDSeries2", "MOTHERBOARDSeries3", "MOTHERBOARDSeries4" } },
            { "Chipset", new string[] { "--Not selected--", "MOTHERBOARDChipset1", "MOTHERBOARDChipset2", "MOTHERBOARDChipset3" } },
            { "CPU company", new string[] { "--Not selected--", "Intel", "AMD" } },
            { "Formfactor", new string[] { "--Not selected--", "MOTHERBOARDFormfactor1", "MOTHERBOARDFormfactor2"} },
            { "Memory", new string[] { "--Not selected--", "256", "512", "1024", "2048" } },
            { "Maximum memory", new string[] { "--Not selected--", "512", "1024", "2048" } },
            { "Memory chanels amount", new string[] { "--Not selected--", "2", "4", "6", "8" } },
            { "Memory slots amount", new string[] { "--Not selected--", "2", "4" } },
            { "Slots amount", new string[] { "--Not selected--", "2", "4", "6", "8", "10" } },
            { "RAM max frequency", new string[] { "--Not selected--", "1.8", "2.2", "3.4" } },
            { "Socket", new string[] { "--Not selected--", "MOTHERBOARDSocket1", "MOTHERBOARDSocket2" } }
        };
        public ObservableCollection<RAM> RAMs { get; set; } = new ObservableCollection<RAM>()
        {

        };
        public Dictionary<string, IEnumerable<string>> RAMFields { get; set; }
        public ObservableCollection<SSD> SSDs { get; set; } = new ObservableCollection<SSD>()
        {

        };
        public Dictionary<string, IEnumerable<string>> SSDFields { get; set; }
        public ObservableCollection<Videocard> Videocards { get; set; } = new ObservableCollection<Videocard>()
        {

        };
        public Dictionary<string, IEnumerable<string>> VIDEOCARDSFields { get; set; }

        private Dictionary<int, Dictionary<string, int>> _selectedFields = new Dictionary<int, Dictionary<string, int>>()
        {
            { 0, new Dictionary<string, int>() },
            { 1, new Dictionary<string, int>() },
            { 2, new Dictionary<string, int>() },
            { 3, new Dictionary<string, int>() },
            { 4, new Dictionary<string, int>() },
            { 5, new Dictionary<string, int>() },
            { 6, new Dictionary<string, int>() },
            { 7, new Dictionary<string, int>() },
            { 8, new Dictionary<string, int>() }
        };

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
