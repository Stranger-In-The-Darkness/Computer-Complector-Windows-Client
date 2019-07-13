using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

using System.Web;
using System.Net;
using System.IO;

using ViewModel.Interfaces;

using m = Model;

namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public class Selection
        {
            public IEnumerable<CPU>         Item1 { get; internal set; }
            public IEnumerable<Motherboard> Item2 { get; internal set; }
            public IEnumerable<Videocard>   Item3 { get; internal set; }
            public IEnumerable<RAM>         Item4 { get; internal set; }
            public IEnumerable<Charger>     Item5 { get; internal set; }
            public IEnumerable<Cooler>      Item6 { get; internal set; }
            public IEnumerable<SSD>         Item7 { get; internal set; }
            public IEnumerable<HDD>         Item8 { get; internal set; }
            public IEnumerable<Body>        Item9 { get; internal set; }
        }

        private string _culture = string.Empty;

        private IDialogService _dialogService;

        public ViewModel(IDialogService service, string culture = "en")
        {
            _culture = culture;
            _dialogService = service;
            _model = new m.Model("", culture);
            _model.PropertyChanged += OnModelPropertyChanged;
            _model.Initialize();
        }

        public ViewModel(IDialogService service, string serviceUri, string culture = "en")
        {
            _culture = culture;
            _dialogService = service;
            _model = new m.Model(serviceUri, culture);
            _model.PropertyChanged += OnModelPropertyChanged;
            _model.Initialize();
        }

        private readonly string[] _type = new string[]
        {
            "cpu",
            "motherboard",
            "videocard",
            "ram",
            "charger",
            "cooler",
            "ssd",
            "hdd",
            "body"
        };

        private int _selectedTab = 0;
        public int SelectedTab { get => _selectedTab; set { _selectedTab = value; OnPropertyChanged("SelectedTab"); } }

        public bool OfflineMode { get; private set; } = false;

        private RelayCommand changeTab;
        public RelayCommand ChangeTab
        {
            get
            {
                return changeTab ??
                    (changeTab = new RelayCommand(
                        async obj =>
                        {
                            _model.Clear(_type[SelectedTab]);

                            int selected = int.Parse(obj.ToString());
                            SelectedTab = selected;

                            await _model.UpdateData(_type[SelectedTab]);
                        },
                        obj =>
                        {
                            return int.Parse(obj.ToString()) >= 0 && int.Parse(obj.ToString()) < 9;
                        }));
            }
        }

        private RelayCommand _selectFilter;
        public RelayCommand SelectFilter
        {
            get
            {
                return _selectFilter ??
                    (_selectFilter = new RelayCommand(
                        async (obj) =>
                        {
                            var values = (Tuple<object, object>)obj;

                            switch (_selectedTab)
                            {
                                case 0:
                                {
                                    try
                                    {
                                        await _model.ToggleFilter(
                                            "cpu", 
                                            values.Item1 as string, 
                                            values.Item2 as string);
                                    }
                                    catch { }
                                }
                                break;
                                case 1:
                                {
                                    try
                                    {
                                        await _model.ToggleFilter(
                                            "motherboard",
                                            values.Item1 as string,
                                            values.Item2 as string);
                                    }
                                    catch { }
                                }
                                break;
                                case 2:
                                {
                                    try
                                    {
                                        await _model.ToggleFilter(
                                            "videocard",
                                            values.Item1 as string,
                                            values.Item2 as string);
                                    }
                                    catch { }
                                }
                                break;
                                case 3:
                                {
                                    try
                                    {
                                        await _model.ToggleFilter(
                                            "ram",
                                            values.Item1 as string, 
                                            values.Item2 as string);
                                    }
                                    catch { }
                                }
                                break;
                                case 4:
                                {
                                    try
                                    {
                                        await _model.ToggleFilter(
                                            "charger",
                                            values.Item1 as string,
                                            values.Item2 as string);
                                    }
                                    catch { }
                                }
                                break;
                                case 5:
                                {
                                    try
                                    {
                                        await _model.ToggleFilter(
                                            "cooler",
                                            values.Item1 as string,
                                            values.Item2 as string);
                                    }
                                    catch { }
                                }
                                break;
                                case 6:
                                {
                                    try
                                    {
                                        await _model.ToggleFilter(
                                            "ssd",
                                            values.Item1 as string,
                                            values.Item2 as string);
                                    }
                                    catch { }
                                }
                                break;
                                case 7:
                                {
                                    try
                                    {
                                        await _model.ToggleFilter(
                                            "hdd",
                                            values.Item1 as string,
                                            values.Item2 as string);
                                    }
                                    catch { }
                                }
                                break;
                                case 8:
                                {
                                    try
                                    {
                                        await _model.ToggleFilter(
                                            "body",
                                            values.Item1 as string,
                                            values.Item2 as string);
                                    }
                                    catch { }
                                }
                                break;
                            }
                        },
                        (obj) =>
                        {
                            return true;
                        }));
            }
        }

        private RelayCommand _selectItem;
        public RelayCommand SelectItem
        {
            get
            {
                return _selectItem ??
                    (_selectItem = new RelayCommand(
                        (obj) =>
                        {
                            _model.SelectItem(_type[_selectedTab], (int)obj);
                        },
                        (obj) =>
                        {
                            return true;
                        }
                        ));
            }
        }

        private RelayCommand _deselectItem;
        public RelayCommand DeselectItem
        {
            get
            {
                return _deselectItem ??
                    (_deselectItem = new RelayCommand(
                        (obj) =>
                        {
                            _model.ClearSelectedItem(_type[int.Parse(obj.ToString())]);
                        },
                        (obj) =>
                        {
                            return int.TryParse(obj.ToString(), out _);
                        }
                        ));
            }
        }

        private RelayCommand _openFile;
        public RelayCommand OpenFile
        {
            get
            {
                return _openFile ??
                    (_openFile = new RelayCommand(
                        (obj) => {
                            try
                            {
                                if (_dialogService.OpenFileDialog() == true)
                                {
                                    _model.OpenSelection(_dialogService.FilePath, _dialogService.FilePath.Split('.').Last());
                                }
                            }
                            catch (Exception ex)
                            {
                                _dialogService.ShowMessage(ex.Message);
                            }
                        },
                        (obj) => { return true; }));
            }
        }

        private RelayCommand _saveFile;
        public RelayCommand SaveFile
        {
            get
            {
                return _saveFile ??
                    (_saveFile = new RelayCommand(
                        (obj) => {
                            try
                            {
                                if (_dialogService.SaveFileDialog() == true)
                                {
                                    _model.SaveSelection(_dialogService.FilePath, _dialogService.FilePath.Split('.').Last());
                                }
                            }
                            catch (Exception ex)
                            {
                                _dialogService.ShowMessage(ex.Message);
                            }
                        },
                        (obj) => {
                            return SelectedBody != null ||
                            SelectedCharger != null ||
                            SelectedCooler != null ||
                            SelectedCpu != null ||
                            SelectedHdd != null ||
                            SelectedMotherboard != null ||
                            SelectedRam != null ||
                            SelectedSsd != null ||
                            SelectedVideocard != null;
                        }));
            }
        }

        private RelayCommand _changeCulture;
        public RelayCommand ChangeCulture
        {
            get
            {
                return _changeCulture ??
                    (_changeCulture = new RelayCommand(
                        (obj)=>
                        {
                            if (obj as string != null)
                            {
                                ChangeModelCulture(obj as string);
                            }
                        },
                        _ => { return true; }));
            }
        }

        private m.Model _model;

        public ObservableCollection<Body> Bodies { get; set; } = new ObservableCollection<Body>();
        public ObservableCollection<Charger> Chargers { get; set; } = new ObservableCollection<Charger>();
        public ObservableCollection<Cooler> Coolers { get; set; } = new ObservableCollection<Cooler>();
        public ObservableCollection<CPU> CPUs { get; set; } = new ObservableCollection<CPU>();
        public ObservableCollection<HDD> HDDs { get; set; } = new ObservableCollection<HDD>();
        public ObservableCollection<Motherboard> Motherboards { get; set; } = new ObservableCollection<Motherboard>();
        public ObservableCollection<RAM> RAMs { get; set; } = new ObservableCollection<RAM>();
        public ObservableCollection<SSD> SSDs { get; set; } = new ObservableCollection<SSD>();
        public ObservableCollection<Videocard> Videocards { get; set; } = new ObservableCollection<Videocard>();

        private Body _selectedBody = null;
        public Body SelectedBody
        {
            get => _selectedBody;
            set
            {
                if (value != null)
                {
                    if (_selectedBody != null)
                    {
                        _selectedBody.IsSelected = false;
                        var i = Bodies.IndexOf(_selectedBody);
                        if (i != -1)
                        {
                            Bodies[i].IsSelected = false;
                        }
                    }
                    {
                        _selectedBody = value;
                        var i = Bodies.IndexOf(_selectedBody);
                        if (i != -1)
                        {
                            Bodies[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("Bodies");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedBody");
                    OnPropertyChanged("SelectedBodyAsEnum");
                }
            }
        }
        public IEnumerable<Body> SelectedBodyAsEnum
        {
            get
            {
                if (_selectedBody != null)
                {
                    yield return _selectedBody;
                }
                yield break;
            }
        }

        private Charger _selectedCharger = null;
        public Charger SelectedCharger
        {
            get => _selectedCharger;
            set
            {
                if (value != null)
                {
                    if (_selectedCharger != null)
                    {
                        _selectedCharger.IsSelected = false;
                        var i = Chargers.IndexOf(_selectedCharger);
                        if (i != -1)
                        {
                            Chargers[i].IsSelected = false;
                        }
                    }
                    {
                        _selectedCharger = value;
                        var i = Chargers.IndexOf(_selectedCharger);
                        if (i != -1)
                        {
                            Chargers[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("Chargers");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedCharger");
                    OnPropertyChanged("SelectedChargerAsEnum");
                }
            }
        }
        public IEnumerable<Charger> SelectedChargerAsEnum
        {
            get
            {
                if (_selectedCharger != null)
                {
                    yield return _selectedCharger;
                }
                yield break;
            }
        }

        private Cooler _selectedCooler = null;
        public Cooler SelectedCooler
        {
            get => _selectedCooler;
            set
            {
                if (value != null)
                {
                    if (_selectedCooler != null)
                    {
                        _selectedCooler.IsSelected = false;
                        var i = Coolers.IndexOf(_selectedCooler);
                        if (i != -1)
                        {
                            Coolers[i].IsSelected = false;
                        }
                    }
                    {
                        _selectedCooler = value;
                        var i = Coolers.IndexOf(_selectedCooler);
                        if (i != -1)
                        {
                            Coolers[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("Coolers");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedCooler");
                    OnPropertyChanged("SelectedCoolerAsEnum");
                }
            }
        }
        public IEnumerable<Cooler> SelectedCoolerAsEnum
        {
            get
            {
                if (_selectedCooler != null)
                {
                    yield return _selectedCooler;
                }
                yield break;
            }
        }

        private CPU _selectedCpu = null;
        public CPU SelectedCpu
        {
            get => _selectedCpu;
            set
            {
                if (value != null)
                {
                    if (_selectedCpu != null)
                    {
                        _selectedCpu.IsSelected = false;
                        var i = CPUs.IndexOf(_selectedCpu);
                        if (i != -1)
                        {
                            CPUs[i].IsSelected = false;
                        }
                    }
                    {
                        _selectedCpu = value;
                        var i = CPUs.IndexOf(_selectedCpu);
                        if (i != -1)
                        {
                            CPUs[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("CPUs");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedCpu");
                    OnPropertyChanged("SelectedCpuAsEnum");
                }
            }
        }
        public IEnumerable<CPU> SelectedCpuAsEnum
        {
            get
            {
                if (_selectedCpu != null)
                {
                    yield return _selectedCpu;
                }
                yield break;
            }
        }

        private HDD _selectedHdd = null;
        public HDD SelectedHdd
        {
            get => _selectedHdd;
            set
            {
                if (value != null)
                {
                    if (_selectedHdd != null)
                    {
                        _selectedHdd.IsSelected = false;
                        var i = HDDs.IndexOf(_selectedHdd);
                        if (i != -1)
                        {
                            HDDs[i].IsSelected = false;
                        }
                    }
                    {
                        _selectedHdd = value;
                        var i = HDDs.IndexOf(_selectedHdd);
                        if (i != -1)
                        {
                            HDDs[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("HDDs");
                    OnPropertyChanged("SelectedHdd");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedHddAsEnum");
                }
            }
        }
        public IEnumerable<HDD> SelectedHddAsEnum
        {
            get
            {
                if (_selectedHdd != null)
                {
                    yield return _selectedHdd;
                }
                yield break;
            }
        }

        private Motherboard _selectedMotherboard = null;
        public Motherboard SelectedMotherboard
        {
            get => _selectedMotherboard;
            set
            {
                if (value != null)
                {
                    if (_selectedMotherboard != null)
                    {
                        _selectedMotherboard.IsSelected = false;
                        var i = Motherboards.IndexOf(_selectedMotherboard);
                        if (i != -1)
                        {
                            Motherboards[i].IsSelected = false;
                        }
                    }
                    {
                        _selectedMotherboard = value;
                        var i = Motherboards.IndexOf(_selectedMotherboard);
                        if (i != -1)
                        {
                            Motherboards[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("Motherboards");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedMotherboard");
                    OnPropertyChanged("SelectedMotherboardAsEnum");
                }
            }
        }
        public IEnumerable<Motherboard> SelectedMotherboardAsEnum
        {
            get
            {
                if (_selectedMotherboard != null)
                {
                    yield return _selectedMotherboard;
                }
                yield break;
            }
        }

        private RAM _selectedRam = null;
        public RAM SelectedRam
        {
            get => _selectedRam;
            set
            {
                if (value != null)
                {
                    if (_selectedRam != null)
                    {
                        _selectedRam.IsSelected = false;
                        var i = RAMs.IndexOf(_selectedRam);
                        if (i != -1)
                        {
                            RAMs[i].IsSelected = false;
                        }
                    }
                    {
                        _selectedRam = value;
                        var i = RAMs.IndexOf(_selectedRam);
                        if (i != -1)
                        {
                            RAMs[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("RAMs");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedRam");
                    OnPropertyChanged("SelectedRamAsEnum");
                }
            }
        }
        public IEnumerable<RAM> SelectedRamAsEnum
        {
            get
            {
                if (_selectedRam != null)
                {
                    yield return _selectedRam;
                }
                yield break;
            }
        }

        private SSD _selectedSsd = null;
        public SSD SelectedSsd
        {
            get => _selectedSsd;
            set
            {
                if (value != null)
                {
                    if (_selectedSsd != null)
                    {
                        _selectedSsd.IsSelected = false;
                        var i = SSDs.IndexOf(_selectedSsd);
                        if (i != -1)
                        {
                            SSDs[i].IsSelected = false;
                        }
                    }
                    {
                        _selectedSsd = value;
                        var i = SSDs.IndexOf(_selectedSsd);
                        if (i != -1)
                        {
                            SSDs[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("SSDs");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedSsd");
                    OnPropertyChanged("SelectedSsdAsEnum");
                }
            }
        }
        public IEnumerable<SSD> SelectedSsdAsEnum
        {
            get
            {
                if (_selectedSsd != null)
                {
                    yield return _selectedSsd;
                }
                yield break;
            }
        }

        private Videocard _selectedVideocard = null;
        public Videocard SelectedVideocard
        {
            get => _selectedVideocard;
            set
            {
                if (value != null)
                {
                    if (_selectedVideocard != null)
                    {
                        _selectedVideocard.IsSelected = false;
                        var i = Videocards.IndexOf(_selectedVideocard);
                        if (i != -1)
                        {
                            Videocards[i].IsSelected = false;
                        }
                    }
                    {
                        _selectedVideocard = value;
                        var i = Videocards.IndexOf(_selectedVideocard);
                        if (i != -1)
                        {
                            Videocards[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("Videocards");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedVideocard");
                    OnPropertyChanged("SelectedVideocardAsEnum");
                }
            }
        }
        public IEnumerable<Videocard> SelectedVideocardAsEnum
        {
            get
            {
                if (_selectedVideocard != null)
                {
                    yield return _selectedVideocard;
                }
                yield break;
            }
        }

        public Selection Selected
        {
            get
            {
                return new Selection() {
                        Item1 = SelectedCpuAsEnum,
                        Item2 = SelectedMotherboardAsEnum,
                        Item3 = SelectedVideocardAsEnum,
                        Item4 = SelectedRamAsEnum,
                        Item5 = SelectedChargerAsEnum,
                        Item6 = SelectedCoolerAsEnum,
                        Item7 = SelectedSsdAsEnum,
                        Item8 = SelectedHddAsEnum,
                        Item9 = SelectedBodyAsEnum
                    };
            }
        }

        private Dictionary<string, Option> _bodyFields = new Dictionary<string, Option>();
        public Dictionary<string, Option> BODYFields          { get => _bodyFields; set { if (value != null) { _bodyFields = value; OnPropertyChanged("BODYFields"); } } }

        private Dictionary<string, Option> _chargerFields = new Dictionary<string, Option>();
        public Dictionary<string, Option> CHARGERFields       { get => _chargerFields; set { if (value != null) { _chargerFields = value; OnPropertyChanged("CHARGERFields"); } } }

        private Dictionary<string, Option> _coolerFields = new Dictionary<string, Option>();
        public Dictionary<string, Option> COOLERFields        { get => _coolerFields; set { if (value != null) { _coolerFields = value; OnPropertyChanged("COOLERFields"); } } }

        private Dictionary<string, Option> _cpuFields = new Dictionary<string, Option>();
        public Dictionary<string, Option> CPUFields           { get => _cpuFields; set { if (value != null) { _cpuFields = value; OnPropertyChanged("CPUFields"); } } }

        private Dictionary<string, Option> _hddFields = new Dictionary<string, Option>();
        public Dictionary<string, Option> HDDFields           { get => _hddFields; set { if (value != null) { _hddFields = value; OnPropertyChanged("HDDFields"); } } }

        private Dictionary<string, Option> _motherboardFields = new Dictionary<string, Option>();
        public Dictionary<string, Option> MOTHERBOARDFields   { get => _motherboardFields; set { if (value != null) { _motherboardFields = value; OnPropertyChanged("MOTHERBOARDFields"); } } }

        private Dictionary<string, Option> _ramFields = new Dictionary<string, Option>();
        public Dictionary<string, Option> RAMFields           { get => _ramFields; set { if (value != null) { _ramFields = value; OnPropertyChanged("RAMFields"); } } }

        private Dictionary<string, Option> _ssdFields = new Dictionary<string, Option>();
        public Dictionary<string, Option> SSDFields           { get => _ssdFields; set { if (value != null) { _ssdFields = value; OnPropertyChanged("SSDFields"); } } }

        private Dictionary<string, Option> _videocardFields = new Dictionary<string, Option>();
        public Dictionary<string, Option> VIDEOCARDFields     { get => _videocardFields; set { if (value != null) { _videocardFields = value; OnPropertyChanged("VIDEOCARDFields"); } } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private async Task LoadEntries()
        {
            await Task.Factory.StartNew(
                async () => 
                {
                    switch (SelectedTab)
                    {
                        case 0:
                        {
                            await _model.LoadAllEntries("body");
                        }
                        break;
                        case 1:
                        {
                            await _model.LoadAllEntries("charger");
                        }
                        break;
                        case 2:
                        {
                            await _model.LoadAllEntries("cooler");
                        }
                        break;
                        case 3:
                        {
                            await _model.LoadAllEntries("cpu");
                        }
                        break;
                        case 4:
                        {
                            await _model.LoadAllEntries("hdd");
                        }
                        break;
                        case 5:
                        {
                            await _model.LoadAllEntries("motherboard");
                        }
                        break;
                        case 6:
                        {
                            await _model.LoadAllEntries("ram");
                        }
                        break;
                        case 7:
                        {
                            await _model.LoadAllEntries("ssd");
                        }
                        break;
                        case 8:
                        {
                            await _model.LoadAllEntries("videocard");
                        }
                        break;
                    }
                }
                );
        }

        private async Task LoadFields()
        {
            await Task.Factory.StartNew(
                async () =>
                {
                    switch (SelectedTab)
                    {
                        case 0:
                        {
                            await _model.LoadFields("cpu");
                        }
                        break;
                        case 1:
                        {
                            await _model.LoadFields("motherboard");
                        }
                        break;
                        case 2:
                        {
                            await _model.LoadFields("videocard");
                        }
                        break;
                        case 3:
                        {
                            await _model.LoadFields("ram");
                        }
                        break;
                        case 4:
                        {
                            await _model.LoadFields("charger");
                        }
                        break;
                        case 5:
                        {
                            await _model.LoadFields("cooler");
                        }
                        break;
                        case 6:
                        {
                            await _model.LoadFields("ssd");
                        }
                        break;
                        case 7:
                        {
                            await _model.LoadFields("hdd");
                        }
                        break;
                        case 8:
                        {
                            await _model.LoadFields("body");
                        }
                        break;
                    }
                }
                );
        }

        private void ChangeModelCulture(string newCulture)
        {
            if (newCulture != null)
            {
                _model.ChangeCulture(newCulture);
            }
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Bodies":
                {
                    Bodies.Clear();
                    foreach (m.Body b in _model.Bodies)
                    {
                        if (!Bodies.Contains(b))
                        {
                            Bodies.Add(b);
                        }
                    }
                    if (_selectedBody != null)
                    {
                        var i = Bodies.IndexOf(_selectedBody);
                        if (i != -1)
                        {
                            Bodies[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("Bodies");
                }
                break;
                case "Chargers":
                {
                    Chargers.Clear();
                    foreach (m.Charger b in _model.Chargers)
                    {
                        if (!Chargers.Contains(b))
                        {
                            Chargers.Add(b);
                        }
                    }
                    if (_selectedCharger != null)
                    {
                        var i = Chargers.IndexOf(_selectedCharger);
                        if (i != -1)
                        {
                            Chargers[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("Chargers");
                }
                break;
                case "Coolers":
                {
                    Coolers.Clear();
                    foreach (m.Cooler b in _model.Coolers)
                    {
                        if (!Coolers.Contains(b))
                        {
                            Coolers.Add(b);
                        }
                    }
                    if (_selectedCooler != null)
                    {
                        var i = Coolers.IndexOf(_selectedCooler);
                        if (i != -1)
                        {
                            Coolers[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("Coolers");
                }
                break;
                case "Cpus":
                {
                    CPUs.Clear();
                    foreach (m.CPU b in _model.Cpus)
                    {
                        if (!CPUs.Contains(b))
                        {
                            CPUs.Add(b);
                        }
                    }
                    if (_selectedCpu != null)
                    {
                        var i = CPUs.IndexOf(_selectedCpu);
                        if (i != -1)
                        {
                            CPUs[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("CPUs");
                }
                break;
                case "Hdds":
                {
                    HDDs.Clear();
                    foreach (m.HDD b in _model.Hdds)
                    {
                        if (!HDDs.Contains(b))
                        {
                            HDDs.Add(b);
                        }
                    }
                    if (_selectedHdd != null)
                    {
                        var i = HDDs.IndexOf(_selectedHdd);
                        if (i != -1)
                        {
                            HDDs[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("HDDs");
                }
                break;
                case "Motherboards":
                {
                    Motherboards.Clear();
                    foreach (m.Motherboard b in _model.Motherboards)
                    {
                        if (!Motherboards.Contains(b))
                        {
                            Motherboards.Add(b);
                        }
                    }
                    if (_selectedMotherboard != null)
                    {
                        var i = Motherboards.IndexOf(_selectedMotherboard);
                        if (i != -1)
                        {
                            Motherboards[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("Motherboards");
                }
                break;
                case "Rams":
                {
                    RAMs.Clear();
                    foreach (m.RAM b in _model.Rams)
                    {
                        if (!RAMs.Contains(b))
                        {
                            RAMs.Add(b);
                        }
                    }
                    if (_selectedRam != null)
                    {
                        var i = RAMs.IndexOf(_selectedRam);
                        if (i != -1)
                        {
                            RAMs[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("RAMs");
                }
                break;
                case "Ssds":
                {
                    SSDs.Clear();
                    foreach (m.SSD b in _model.Ssds)
                    {
                        if (!SSDs.Contains(b))
                        {
                            SSDs.Add(b);
                        }
                    }
                    if (_selectedSsd != null)
                    {
                        var i = SSDs.IndexOf(_selectedSsd);
                        if (i != -1)
                        {
                            SSDs[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("SSDs");
                }
                break;
                case "Videocards":
                {
                    Videocards.Clear();
                    foreach (m.Videocard b in _model.Videocards)
                    {
                        if (!Videocards.Contains(b))
                        {
                            Videocards.Add(b);
                        }
                    }
                    if (_selectedVideocard != null)
                    {
                        var i = Videocards.IndexOf(_selectedVideocard);
                        if (i != -1)
                        {
                            Videocards[i].IsSelected = true;
                        }
                    }
                    OnPropertyChanged("Videocards");
                }
                break;
                case "BodyFields": BODYFields = _model.BodyFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
                break;
                case "ChargerFields": CHARGERFields = _model.ChargerFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
                break;
                case "CoolerFields": COOLERFields = _model.CoolerFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
                break;
                case "CPUFields": CPUFields = _model.CPUFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
                break;
                case "HDDFields": HDDFields = _model.HDDFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
                break;
                case "MotherboardFields": MOTHERBOARDFields = _model.MotherboardFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
                break;
                case "RAMFields": RAMFields = _model.RAMFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
                break;
                case "SSDFields": SSDFields = _model.SSDFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
                break;
                case "VideocardFields": VIDEOCARDFields = _model.VideocardFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
                break;
                case "SelectedBody":
                {
                    if (_model.SelectedBody == null)
                    {
                        _selectedBody = null;
                        try
                        {
                            Bodies.Where((b) => b.IsSelected).First().IsSelected = false;
                        }
                        catch { }
                    }
                    else
                    {
                        SelectedBody = _model.SelectedBody;
                    }
                    OnPropertyChanged("Bodies");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedBody");
                    OnPropertyChanged("SelectedBodyAsEnum");
                }
                break;
                case "SelectedCharger":
                {
                    if (_model.SelectedCharger == null)
                    {
                        _selectedCharger = null;
                        try
                        {
                            Chargers.Where((b) => b.IsSelected).First().IsSelected = false;
                        }
                        catch { }
                    }
                    else
                    {
                        SelectedCharger = _model.SelectedCharger;
                    }
                    OnPropertyChanged("Chargers");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedCharger");
                    OnPropertyChanged("SelectedChargerAsEnum");
                }
                break;
                case "SelectedCooler":
                {
                    if (_model.SelectedCooler == null)
                    {
                        _selectedCooler = null;
                        try
                        {
                            Coolers.Where((b) => b.IsSelected).First().IsSelected = false;
                        }
                        catch { }
                    }
                    else
                    {
                        SelectedCooler = _model.SelectedCooler;
                    }
                    OnPropertyChanged("Coolers");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedCooler");
                    OnPropertyChanged("SelectedCoolerAsEnum");
                }
                break;
                case "SelectedCpu":
                {
                    if (_model.SelectedCpu == null)
                    {
                        _selectedCpu = null;
                        try
                        {
                            CPUs.Where((b) => b.IsSelected).First().IsSelected = false;
                        }
                        catch { }
                    }
                    else
                    {
                        SelectedCpu = _model.SelectedCpu;
                    }
                    OnPropertyChanged("CPUs");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedCpu");
                    OnPropertyChanged("SelectedCpuAsEnum");
                }
                break;
                case "SelectedHdd":
                {
                    if (_model.SelectedHdd == null)
                    {
                        _selectedHdd = null;
                        try
                        {
                            HDDs.Where((b) => b.IsSelected).First().IsSelected = false;
                        }
                        catch { }
                    }
                    else
                    {
                        SelectedHdd = _model.SelectedHdd;
                    }
                    OnPropertyChanged("HDDs");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedHdd");
                    OnPropertyChanged("SelectedHddAsEnum");
                }
                break;
                case "SelectedMotherboard":
                {
                    if (_model.SelectedMotherboard == null)
                    {
                        _selectedMotherboard = null;
                        try
                        {
                            Motherboards.Where((b) => b.IsSelected).First().IsSelected = false;
                        }
                        catch { }
                    }
                    else
                    {
                        SelectedMotherboard = _model.SelectedMotherboard;
                    }
                    OnPropertyChanged("Motherboards");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedMotherboard");
                    OnPropertyChanged("SelectedMotherboardAsEnum");
                }
                break;
                case "SelectedRam":
                {
                    if (_model.SelectedRam == null)
                    {
                        _selectedRam = null;
                        try
                        {
                            RAMs.Where((b) => b.IsSelected).First().IsSelected = false;
                        }
                        catch { }
                    }
                    else
                    {
                        SelectedRam = _model.SelectedRam;
                    }
                    OnPropertyChanged("RAMs");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedRam");
                    OnPropertyChanged("SelectedRamAsEnum");
                }
                break;
                case "SelectedSsd":
                {
                    if (_model.SelectedSsd == null)
                    {
                        _selectedSsd = null;
                        try
                        {
                            SSDs.Where((b) => b.IsSelected).First().IsSelected = false;
                        }
                        catch { }
                    }
                    else
                    {
                        SelectedSsd = _model.SelectedSsd;
                    }
                    OnPropertyChanged("SSDs");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedSsd");
                    OnPropertyChanged("SelectedSsdAsEnum");
                }
                break;
                case "SelectedVideocard":
                {
                    if (_model.SelectedVideocard == null)
                    {
                        _selectedVideocard = null;
                        try
                        {
                            Videocards.Where((b) => b.IsSelected).First().IsSelected = false;
                        }
                        catch { }
                    }
                    else
                    {
                        SelectedVideocard = _model.SelectedVideocard;
                    }
                    OnPropertyChanged("Videocards");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("SelectedVideocard");
                    OnPropertyChanged("SelectedVideocardAsEnum");
                }
                break;
                case "IsFaulted":
                {
                    if (_model.IsFaulted)
                    {
                        OfflineMode = true;
                    }
                }
                break;
                default:
                break;
            }
        }
    }
}
