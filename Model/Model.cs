//#define LOCALHOST
//#define TESTCASE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;

using System.Xml;
using System.Xml.Serialization;

using Newtonsoft.Json;


namespace Model
{
    /// <summary>
    /// MVVM client Model class
    /// </summary>
    public class Model : INotifyPropertyChanged
    {
        [Serializable]
        [XmlRoot]
        public class Selected
        {
            public Body SelectedBody { get; set; }
            public Cooler SelectedCooler { get; set; }
            public Charger SelectedCharger { get; set; }
            public CPU SelectedCPU { get; set; }
            public HDD SelectedHDD { get; set; }
            public Motherboard SelectedMotherboard { get; set; }
            public RAM SelectedRAM { get; set; }
            public SSD SelectedSSD { get; set; }
            public Videocard SelectedVideocard { get; set; }
        }
#if LOCALHOST
        private string _apiUri = "https://localhost:44346/api/components";
#else
        private string _apiUri = null;
#endif
        private string _culture = string.Empty;
        /// <summary>
        /// True, if web request is faulted
        /// </summary>
        public bool IsFaulted { get; private set; } = false;

        private List<Body> _bodies;
        private Body _selectedBody;
        private Dictionary<string, Option> _bodyFields;
        private Dictionary<string, IEnumerable<string>> _selectedBodyFields = new Dictionary<string, IEnumerable<string>>();

        private List<Charger> _chargers;
        private Charger _selectedCharger;
        private Dictionary<string, Option> _chargerFields;
        private Dictionary<string, IEnumerable<string>> _selectedChargerFields = new Dictionary<string, IEnumerable<string>>();

        private List<Cooler> _coolers;
        private Cooler _selectedCooler;
        private Dictionary<string, Option> _coolerFields;
        private Dictionary<string, IEnumerable<string>> _selectedCoolerFields = new Dictionary<string, IEnumerable<string>>();

        private List<CPU> _cpus; 
        private CPU _selectedCpu;
        private Dictionary<string, Option> _cpuFields;
        private Dictionary<string, IEnumerable<string>> _selectedCpuFields = new Dictionary<string, IEnumerable<string>>();

        private List<HDD> _hdds;
        private HDD _selectedHdd;
        private Dictionary<string, Option> _hddFields;
        private Dictionary<string, IEnumerable<string>> _selectedHddFields = new Dictionary<string, IEnumerable<string>>();

        private List<Motherboard> _motherboards;
        private Motherboard _selectedMotherboard;
        private Dictionary<string, Option> _motherboardFields;
        private Dictionary<string, IEnumerable<string>> _selectedMotherboardFields = new Dictionary<string, IEnumerable<string>>();

        private List<RAM> _rams;
        private RAM _selectedRam;
        private Dictionary<string, Option> _ramFields;
        private Dictionary<string, IEnumerable<string>> _selectedRamFields = new Dictionary<string, IEnumerable<string>>();

        private List<SSD> _ssds;
        private SSD _selectedSsd;
        private Dictionary<string, Option> _ssdFields;
        private Dictionary<string, IEnumerable<string>> _selectedSsdFields = new Dictionary<string, IEnumerable<string>>();

        private List<Videocard> _videocards;
        private Videocard _selectedVideocard;
        private Dictionary<string, Option> _videocardFields;
        private Dictionary<string, IEnumerable<string>> _selectedVideocardFields = new Dictionary<string, IEnumerable<string>>();

        public List<Body> Bodies { get => _bodies; set { _bodies = value; OnPropertyChanged("Bodies"); } }
        public Body SelectedBody { get => _selectedBody; set { if (value != null) { _selectedBody = value; OnPropertyChanged("SelectedBody"); } } }
        /// <summary>
        /// Field, description, options
        /// </summary>
        public Dictionary<string, Option> BodyFields { get => _bodyFields; set { _bodyFields = value; OnPropertyChanged("BodyFields"); } }

        public List<Charger> Chargers { get => _chargers; set { _chargers = value; OnPropertyChanged("Chargers"); } }
        public Charger SelectedCharger { get => _selectedCharger; set { if (value != null) { _selectedCharger = value; OnPropertyChanged("SelectedCharger"); } } }
        /// <summary>
        /// Field, description, options
        /// </summary>
        public Dictionary<string, Option> ChargerFields { get => _chargerFields; set { _chargerFields = value; OnPropertyChanged("ChargerFields"); } }

        public List<Cooler> Coolers { get => _coolers; set { _coolers = value; OnPropertyChanged("Coolers"); } }
        public Cooler SelectedCooler { get => _selectedCooler; set { if (value != null) { _selectedCooler = value; OnPropertyChanged("SelectedCooler"); } } }
        /// <summary>
        /// Field, description, options
        /// </summary>
        public Dictionary<string, Option> CoolerFields { get => _coolerFields; set { _coolerFields = value; OnPropertyChanged("CoolerFields"); } }

        public List<CPU> Cpus { get => _cpus; set { _cpus = value; OnPropertyChanged("Cpus"); } }
        public CPU SelectedCpu { get => _selectedCpu; set { if (value != null) { _selectedCpu = value; OnPropertyChanged("SelectedCpu"); } } }
        /// <summary>
        /// Field, description, options
        /// </summary>
        public Dictionary<string, Option> CPUFields { get => _cpuFields; set { _cpuFields = value; OnPropertyChanged("CpuFields"); } }

        public List<HDD> Hdds { get => _hdds; set { _hdds = value; OnPropertyChanged("Hdds"); } }
        public HDD SelectedHdd { get => _selectedHdd; set { if (value != null) { _selectedHdd = value; OnPropertyChanged("SelectedHdd"); } } }
        /// <summary>
        /// Field, description, options
        /// </summary>
        public Dictionary<string, Option> HDDFields { get => _hddFields; set { _hddFields = value; OnPropertyChanged("HddFields"); } }

        public List<RAM> Rams { get => _rams; set { _rams = value; OnPropertyChanged("Rams"); } }
        public RAM SelectedRam { get => _selectedRam; set { if (value != null) { _selectedRam = value; OnPropertyChanged("SelectedRam"); } } }
        /// <summary>
        /// Field, description, options
        /// </summary>
        public Dictionary<string, Option> RAMFields { get => _ramFields; set { _ramFields = value; OnPropertyChanged("RamFields"); } }

        public List<SSD> Ssds { get => _ssds; set { _ssds = value; OnPropertyChanged("Ssds"); } }
        public SSD SelectedSsd { get => _selectedSsd; set { if (value != null) { _selectedSsd = value; OnPropertyChanged("SelectedSsd"); } } }
        /// <summary>
        /// Field, description, options
        /// </summary>
        public Dictionary<string, Option> SSDFields { get => _ssdFields; set { _ssdFields = value; OnPropertyChanged("SsdFields"); } }

        public List<Motherboard> Motherboards { get => _motherboards; set { _motherboards = value; OnPropertyChanged("Motherboards"); } }
        /// <summary>
        /// Field, description, options
        /// </summary>
        public Motherboard SelectedMotherboard { get => _selectedMotherboard; set { if (value != null) { _selectedMotherboard = value; OnPropertyChanged("SelectedMotherboard"); } } }
        public Dictionary<string, Option> MotherboardFields { get => _motherboardFields; set { _motherboardFields = value; OnPropertyChanged("MotherboardFields"); } }

        public List<Videocard> Videocards { get => _videocards; set { _videocards = value; OnPropertyChanged("Videocards"); } }
        public Videocard SelectedVideocard { get => _selectedVideocard; set { if (value != null) { _selectedVideocard = value; OnPropertyChanged("SelectedVideocard"); } } }
        /// <summary>
        /// Field, description, options
        /// </summary>
        public Dictionary<string, Option> VideocardFields { get => _videocardFields; set { _videocardFields = value; OnPropertyChanged("VideocardFields"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public Model(string apiUri, string culture = "en")
        {
            _bodies = new List<Body>();
            _chargers = new List<Charger>();
            _coolers = new List<Cooler>();
            _cpus = new List<CPU>();
            _hdds = new List<HDD>();
            _motherboards = new List<Motherboard>();
            _rams = new List<RAM>();
            _ssds = new List<SSD>();
            _videocards = new List<Videocard>();

#if !LOCALHOST
            _apiUri = apiUri;
#endif

            _culture = culture;
        }

        /// <summary>
        /// Initializes loading of entries and filters
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            await LoadFields("body");
            await LoadFields("charger");
            await LoadFields("cooler");
            await LoadFields("cpu");
            await LoadFields("hdd");
            await LoadFields("motherboard");
            await LoadFields("ram");
            await LoadFields("ssd");
            await LoadFields("videocard");

            await LoadAllEntries("body");
            await LoadAllEntries("charger");
            await LoadAllEntries("cooler");
            await LoadAllEntries("cpu");
            await LoadAllEntries("hdd");
            await LoadAllEntries("motherboard");
            await LoadAllEntries("ram");
            await LoadAllEntries("ssd");
            await LoadAllEntries("videocard");
        }

        /// <summary>
        /// Loads all records of a specified component
        /// </summary>
        /// <param name="type">Component name</param>
        /// <returns></returns>
        public async Task LoadAllEntries(string type)
        {
            switch (type)
            {
                case "body":
                {
                    string request = $"{_apiUri}/bodies";
                    
                    HttpWebRequest get = (HttpWebRequest)WebRequest.Create(request);

                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)await get.GetResponseAsync();
                    }
                    catch (WebException)
                    {
                        //If request if faulted due to different reasons, activate "offline-mode"
                        IsFaulted = true;
                        OnPropertyChanged("IsFaulted");
                        return;
                    }

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            //Clear current list if not null
                            //then add new records
                            Bodies?.Clear();
                            string res = await reader.ReadToEndAsync();
                            foreach (var e in JsonConvert.DeserializeObject<IEnumerable<Body>>(res))
                            {
                                if (Bodies != null && !Bodies.Contains(e))
                                {
                                    Bodies.Add(e);
                                }
                            }
                            OnPropertyChanged("Bodies");
                        }
                    }
                }
                break;
                case "charger":
                case "cooler":
                case "cpu":
                case "hdd":
                case "motherboard":
                case "ram":
                case "ssd":
                case "videocard":
                {
                    string request = $"{_apiUri}/{type}s";

                    HttpWebRequest get = (HttpWebRequest)WebRequest.Create(request);

                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)await get.GetResponseAsync();
                    }
                    catch (WebException)
                    {
                        //If request if faulted due to different reasons, activate "offline-mode"
                        IsFaulted = true;
                        OnPropertyChanged("IsFaulted");
                        return;
                    }

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string res = await reader.ReadToEndAsync();
                            switch (type)
                            {
                                case "charger":
                                Chargers?.Clear();
                                foreach (var e in JsonConvert.DeserializeObject<IEnumerable<Charger>>(res))
                                {
                                    if (Chargers!= null && !Chargers.Contains(e))
                                    {
                                        Chargers.Add(e);
                                    }
                                }
                                OnPropertyChanged("Chargers");
                                break;
                                case "cooler":
                                Coolers?.Clear();
                                foreach (var e in JsonConvert.DeserializeObject<IEnumerable<Cooler>>(res))
                                {
                                    if (Coolers != null && !Coolers.Contains(e))
                                    {
                                        Coolers.Add(e);
                                    }
                                }
                                OnPropertyChanged("Coolers");
                                break;
                                case "cpu":
                                Cpus?.Clear();
                                foreach (var e in JsonConvert.DeserializeObject<IEnumerable<CPU>>(res))
                                {
                                    if (Cpus != null && !Cpus.Contains(e))
                                    {
                                        Cpus.Add(e);
                                    }
                                }
                                OnPropertyChanged("Cpus");
                                break;
                                case "hdd":
                                Hdds?.Clear();
                                foreach (var e in JsonConvert.DeserializeObject<IEnumerable<HDD>>(res))
                                {
                                    if (Hdds != null && !Hdds.Contains(e))
                                    {
                                        Hdds.Add(e);
                                    }
                                }
                                OnPropertyChanged("Hdds");
                                break;
                                case "motherboard":
                                Motherboards?.Clear();
                                foreach (var e in JsonConvert.DeserializeObject<IEnumerable<Motherboard>>(res))
                                {
                                    if (Motherboards != null && !Motherboards.Contains(e))
                                    {
                                        Motherboards.Add(e);
                                    }
                                }
                                OnPropertyChanged("Motherboards");
                                break;
                                case "ram":
                                Rams?.Clear();
                                foreach (var e in JsonConvert.DeserializeObject<IEnumerable<RAM>>(res))
                                {
                                    if (Rams != null && !Rams.Contains(e))
                                    {
                                        Rams.Add(e);
                                    }
                                }
                                OnPropertyChanged("Rams");
                                break;
                                case "ssd":
                                Ssds?.Clear();
                                foreach (var e in JsonConvert.DeserializeObject<IEnumerable<SSD>>(res))
                                {
                                    if (Ssds != null && !Ssds.Contains(e))
                                    {
                                        Ssds.Add(e);
                                    }
                                }
                                OnPropertyChanged("Ssds");
                                break;
                                case "videocard":
                                Videocards?.Clear();
                                foreach (var e in JsonConvert.DeserializeObject<IEnumerable<Videocard>>(res))
                                {
                                    if (Videocards != null && !Videocards.Contains(e))
                                    {
                                        Videocards.Add(e);
                                    }
                                }
                                OnPropertyChanged("Videocards");
                                break;
                            }
                        }
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Loads all filters form specified component
        /// </summary>
        /// <param name="type">component name</param>
        /// <returns></returns>
        public async Task LoadFields(string type)
        {
            if (type == "body" ||
                type == "charger" ||
                type == "cooler" ||
                type == "cpu" ||
                type == "hdd" ||
                type == "motherboard" ||
                type == "ram" ||
                type == "ssd" ||
                type == "videocard")
            {
                string request = $"{_apiUri}/{type}/properties?culture={_culture}";

                HttpWebRequest get = (HttpWebRequest)WebRequest.Create(request);

                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)await get.GetResponseAsync();
                }
                catch (WebException e)
                {
                    var ex = e;
                    IsFaulted = true;
                    OnPropertyChanged("IsFaulted");
                    return;
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string res = await reader.ReadToEndAsync();
                        Dictionary<string, Option> v = null;
                        try
                        {
                            v = JsonConvert.DeserializeObject<Dictionary<string, Option>>(res);
                        }
                        catch (Exception e) { };
                        switch (type)
                        {
                            case "body": BodyFields = v;
                            OnPropertyChanged("BodyFields");
                            break;
                            case "charger": ChargerFields = v;
                            OnPropertyChanged("ChargerFields");
                            break;
                            case "cooler": CoolerFields = v;
                            OnPropertyChanged("CoolerFields");
                            break;
                            case "cpu": CPUFields = v;
                            OnPropertyChanged("CPUFields");
                            break;
                            case "hdd": HDDFields = v;
                            OnPropertyChanged("HDDFields");
                            break;
                            case "motherboard": MotherboardFields = v;
                            OnPropertyChanged("MotherboardFields");
                            break;
                            case "ram": RAMFields = v;
                            OnPropertyChanged("RAMFields");
                            break;
                            case "ssd": SSDFields = v;
                            OnPropertyChanged("SSDFields");
                            break;
                            case "videocard": VideocardFields = v;
                            OnPropertyChanged("VideocardFields");
                            break;
                        }
                    }
                }
            }
        }

        ///// <summary>
        ///// Changes selected option of filter of specific component
        ///// </summary>
        ///// <param name="type">Component name</param>
        ///// <param name="property">Filter name</param>
        ///// <param name="value">Option</param>
        ///// <returns></returns>
        //public async Task ChangeFilter(string type, string property, string value)
        //{
        //    switch (type)
        //    {
        //        case "body":
        //        {
        //            if (!_selectedBodyFields.Keys.Contains(property))
        //            {
        //                _selectedBodyFields.Add(property, value);
        //            }
        //            else
        //            {
        //                _selectedBodyFields[property] = value;
        //            }
        //            await UpdateData(type);
        //        }
        //        break;
        //        case "charger":
        //        {
        //            if (!_selectedChargerFields.Keys.Contains(property))
        //            {
        //                _selectedChargerFields.Add(property, value);
        //            }
        //            else
        //            {
        //                _selectedChargerFields[property] = value;
        //            }
        //            await UpdateData(type);
        //        }
        //        break;
        //        case "cooler":
        //        {
        //            if (!_selectedCoolerFields.Keys.Contains(property))
        //            {
        //                _selectedCoolerFields.Add(property, value);
        //            }
        //            else
        //            {
        //                _selectedCoolerFields[property] = value;
        //            }
        //            await UpdateData(type);
        //        }
        //        break;
        //        case "cpu":
        //        {
        //            if (!_selectedCpuFields.Keys.Contains(property))
        //            {
        //                _selectedCpuFields.Add(property, value);
        //            }
        //            else
        //            {
        //                _selectedCpuFields[property] = value;
        //            }
        //            await UpdateData(type);
        //        }
        //        break;
        //        case "hdd":
        //        {
        //            if (!_selectedHddFields.Keys.Contains(property))
        //            {
        //                _selectedHddFields.Add(property, value);
        //            }
        //            else
        //            {
        //                _selectedHddFields[property] = value;
        //            }
        //            await UpdateData(type);
        //        }
        //        break;
        //        case "motherboard":
        //        {
        //            if (!_selectedMotherboardFields.Keys.Contains(property))
        //            {
        //                _selectedMotherboardFields.Add(property, value);
        //            }
        //            else
        //            {
        //                _selectedMotherboardFields[property] = value;
        //            }
        //            await UpdateData(type);
        //        }
        //        break;
        //        case "ram":
        //        {
        //            if (!_selectedRamFields.Keys.Contains(property))
        //            {
        //                _selectedRamFields.Add(property, value);
        //            }
        //            else
        //            {
        //                _selectedRamFields[property] = value;
        //            }
        //            await UpdateData(type);
        //        }
        //        break;
        //        case "ssd":
        //        {
        //            if (!_selectedSsdFields.Keys.Contains(property))
        //            {
        //                _selectedSsdFields.Add(property, value);
        //            }
        //            else
        //            {
        //                _selectedSsdFields[property] = value;
        //            }
        //            await UpdateData(type);
        //        }
        //        break;
        //        case "videocard":
        //        {
        //            if (!_selectedVideocardFields.Keys.Contains(property))
        //            {
        //                _selectedVideocardFields.Add(property, value);
        //            }
        //            else
        //            {
        //                _selectedVideocardFields[property] = value;
        //            }
        //            await UpdateData(type);
        //        }
        //        break;
        //    }
        //}

        //public async Task ClearFilter(string type, string property)
        //{
        //    switch (type)
        //    {
        //        case "body":
        //        {
        //            if (_selectedBodyFields.Keys.Contains(property))
        //            {
        //                _selectedBodyFields.Remove(property);
        //                await UpdateData(type);
        //            }
        //        }
        //        break;
        //        case "charger":
        //        {
        //            if (_selectedChargerFields.Keys.Contains(property))
        //            {
        //                _selectedChargerFields.Remove(property);
        //                await UpdateData(type);
        //            }
        //        }
        //        break;
        //        case "cooler":
        //        {
        //            if (_selectedCoolerFields.Keys.Contains(property))
        //            {
        //                _selectedCoolerFields.Remove(property);
        //                await UpdateData(type);
        //            }
        //        }
        //        break;
        //        case "cpu":
        //        {
        //            if (_selectedCpuFields.Keys.Contains(property))
        //            {
        //                _selectedCpuFields.Remove(property);
        //                await UpdateData(type);
        //            }
        //        }
        //        break;
        //        case "hdd":
        //        {
        //            if (_selectedHddFields.Keys.Contains(property))
        //            {
        //                _selectedHddFields.Remove(property);
        //                await UpdateData(type);
        //            }
        //        }
        //        break;
        //        case "motherboard":
        //        {
        //            if (_selectedMotherboardFields.Keys.Contains(property))
        //            {
        //                _selectedMotherboardFields.Remove(property);
        //                await UpdateData(type);
        //            }
        //        }
        //        break;
        //        case "ram":
        //        {
        //            if (_selectedRamFields.Keys.Contains(property))
        //            {
        //                _selectedRamFields.Remove(property);
        //                await UpdateData(type);
        //            }
        //        }
        //        break;
        //        case "ssd":
        //        {
        //            if (_selectedSsdFields.Keys.Contains(property))
        //            {
        //                _selectedSsdFields.Remove(property);
        //                await UpdateData(type);
        //            }
        //        }
        //        break;
        //        case "videocard":
        //        {
        //            if (_selectedVideocardFields.Keys.Contains(property))
        //            {
        //                _selectedVideocardFields.Remove(property);
        //                await UpdateData(type);
        //            }
        //        }
        //        break;
        //    }
        //}

        public async Task ToggleFilter(string type, string property, string value)
        {
            switch (type)
            {
                case "body":
                {
                    if (_selectedBodyFields.Keys.Contains(property))
                    {
                        if (_selectedBodyFields[property].Contains(value))
                        {
                            var values = _selectedBodyFields[property].ToList();
                            values.Remove(value);
                            _selectedBodyFields[property] = values;
                        }
                        else
                        {
                            var values = _selectedBodyFields[property].ToList();
                            values.Add(value);
                            _selectedBodyFields[property] = values;
                        }
                    }
                    else
                    {
                        _selectedBodyFields.Add(property, new string[] { value });
                    }
                    await UpdateData(type);
                }
                break;
                case "charger":
                {
                    if (_selectedChargerFields.Keys.Contains(property))
                    {
                        if (_selectedChargerFields[property].Contains(value))
                        {
                            var values = _selectedChargerFields[property].ToList();
                            values.Remove(value);
                            _selectedChargerFields[property] = values;
                        }
                        else
                        {
                            var values = _selectedChargerFields[property].ToList();
                            values.Add(value);
                            _selectedChargerFields[property] = values;
                        }
                    }
                    else
                    {
                        _selectedChargerFields.Add(property, new string[] { value });
                    }
                    await UpdateData(type);
                }
                break;
                case "cooler":
                {
                    if (_selectedCoolerFields.Keys.Contains(property))
                    {
                        if (_selectedCoolerFields[property].Contains(value))
                        {
                            var values = _selectedCoolerFields[property].ToList();
                            values.Remove(value);
                            _selectedCoolerFields[property] = values;
                        }
                        else
                        {
                            var values = _selectedCoolerFields[property].ToList();
                            values.Add(value);
                            _selectedCoolerFields[property] = values;
                        }
                    }
                    else
                    {
                        _selectedCoolerFields.Add(property, new string[] { value });
                    }
                    await UpdateData(type);
                }
                break;
                case "cpu":
                {
                    if (_selectedCpuFields.Keys.Contains(property))
                    {
                        if (_selectedCpuFields[property].Contains(value))
                        {
                            var values = _selectedCpuFields[property].ToList();
                            values.Remove(value);
                            _selectedCpuFields[property] = values;
                        }
                        else
                        {
                            var values = _selectedCpuFields[property].ToList();
                            values.Add(value);
                            _selectedCpuFields[property] = values;
                        }
                    }
                    else
                    {
                        _selectedCpuFields.Add(property, new string[] { value });
                    }
                    await UpdateData(type);
                }
                break;
                case "hdd":
                {
                    if (_selectedHddFields.Keys.Contains(property))
                    {
                        if (_selectedHddFields[property].Contains(value))
                        {
                            var values = _selectedHddFields[property].ToList();
                            values.Remove(value);
                            _selectedHddFields[property] = values;
                        }
                        else
                        {
                            var values = _selectedHddFields[property].ToList();
                            values.Add(value);
                            _selectedHddFields[property] = values;
                        }
                    }
                    else
                    {
                        _selectedHddFields.Add(property, new string[] { value });
                    }
                    await UpdateData(type);
                }
                break;
                case "motherboard":
                {
                    if (_selectedMotherboardFields.Keys.Contains(property))
                    {
                        if (_selectedMotherboardFields[property].Contains(value))
                        {
                            var values = _selectedMotherboardFields[property].ToList();
                            values.Remove(value);
                            _selectedMotherboardFields[property] = values;
                        }
                        else
                        {
                            var values = _selectedMotherboardFields[property].ToList();
                            values.Add(value);
                            _selectedMotherboardFields[property] = values;
                        }
                    }
                    else
                    {
                        _selectedMotherboardFields.Add(property, new string[] { value });
                    }
                    await UpdateData(type);
                }
                break;
                case "ram":
                {
                    if (_selectedRamFields.Keys.Contains(property))
                    {
                        if (_selectedRamFields[property].Contains(value))
                        {
                            var values = _selectedRamFields[property].ToList();
                            values.Remove(value);
                            _selectedRamFields[property] = values;
                        }
                        else
                        {
                            var values = _selectedRamFields[property].ToList();
                            values.Add(value);
                            _selectedRamFields[property] = values;
                        }
                    }
                    else
                    {
                        _selectedRamFields.Add(property, new string[] { value });
                    }
                    await UpdateData(type);
                }
                break;
                case "ssd":
                {
                    if (_selectedSsdFields.Keys.Contains(property))
                    {
                        if (_selectedSsdFields[property].Contains(value))
                        {
                            var values = _selectedSsdFields[property].ToList();
                            values.Remove(value);
                            _selectedSsdFields[property] = values;
                        }
                        else
                        {
                            var values = _selectedSsdFields[property].ToList();
                            values.Add(value);
                            _selectedSsdFields[property] = values;
                        }
                    }
                    else
                    {
                        _selectedSsdFields.Add(property, new string[] { value });
                    }
                    await UpdateData(type);
                }
                break;
                case "videocard":
                {
                    if (_selectedVideocardFields.Keys.Contains(property))
                    {
                        if (_selectedVideocardFields[property].Contains(value))
                        {
                            var values = _selectedVideocardFields[property].ToList();
                            values.Remove(value);
                            _selectedVideocardFields[property] = values;
                        }
                        else
                        {
                            var values = _selectedVideocardFields[property].ToList();
                            values.Add(value);
                            _selectedVideocardFields[property] = values;
                        }
                    }
                    else
                    {
                        _selectedVideocardFields.Add(property, new string[] { value });
                    }
                    await UpdateData(type);
                }
                break;
            }
        }

        public async Task UpdateAllData()
        {
            await UpdateData("body");
            await UpdateData("charger");
            await UpdateData("cooler");
            await UpdateData("cpu");
            await UpdateData("hdd");
            await UpdateData("motherboard");
            await UpdateData("ram");
            await UpdateData("ssd");
            await UpdateData("videocard");
        }

        public async Task UpdateData(string type)
        {
            string request = $"{_apiUri}/{type}?";
            switch (type)
            {
                case "body":
                {
                    if (_selectedBodyFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedBodyFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

                    if (SelectedBody != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-body={SelectedBody.ID}";
                        else
                            request = $"{request}&selected-body={SelectedBody.ID}";
                    }
                    if (SelectedCharger != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-charger={SelectedCharger.ID}";
                        else
                            request = $"{request}&selected-charger={SelectedCharger.ID}";
                    }
                    if (SelectedCooler != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cooler={SelectedCooler.ID}";
                        else
                            request = $"{request}&selected-cooler={SelectedCooler.ID}";
                    }
                    if (SelectedCpu != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cpu={SelectedCpu.ID}";
                        else
                            request = $"{request}&selected-cpu={SelectedCpu.ID}";
                    }
                    if (SelectedHdd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-hdd={SelectedHdd.ID}";
                        else
                            request = $"{request}&selected-hdd={SelectedHdd.ID}";
                    }
                    if (SelectedMotherboard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-motherboard={SelectedMotherboard.ID}";
                        else
                            request = $"{request}&selected-motherboard={SelectedMotherboard.ID}";
                    }
                    if (SelectedRam != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ram={SelectedRam.ID}";
                        else
                            request = $"{request}&selected-ram={SelectedRam.ID}";
                    }
                    if (SelectedSsd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ssd={SelectedSsd.ID}";
                        else
                            request = $"{request}&selected-ssd={SelectedSsd.ID}";
                    }
                    if (SelectedVideocard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-videocard={SelectedVideocard.ID}";
                        else
                            request = $"{request}&selected-videocard={SelectedVideocard.ID}";
                    }

                    HttpWebRequest get = (HttpWebRequest)WebRequest.Create(request);

                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)await get.GetResponseAsync();
                    }
                    catch (WebException)
                    {
                        IsFaulted = true;
                        OnPropertyChanged("IsFaulted");
                        return;
                    }
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            Bodies?.Clear();
                            string res = await reader.ReadToEndAsync();
                            foreach (var e in JsonConvert.DeserializeObject<IEnumerable<Body>>(res))
                            {
                                if (Bodies != null && !Bodies.Contains(e))
                                {
                                    Bodies.Add(e);
                                }
                            }
                            OnPropertyChanged("Bodies");
                        }
                    }
                }
                break;
                case "charger":
                {
                    if (_selectedChargerFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedChargerFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

                    if (SelectedBody != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-body={SelectedBody.ID}";
                        else
                            request = $"{request}&selected-body={SelectedBody.ID}";
                    }
                    if (SelectedCharger != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-charger={SelectedCharger.ID}";
                        else
                            request = $"{request}&selected-charger={SelectedCharger.ID}";
                    }
                    if (SelectedCooler != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cooler={SelectedCooler.ID}";
                        else
                            request = $"{request}&selected-cooler={SelectedCooler.ID}";
                    }
                    if (SelectedCpu != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cpu={SelectedCpu.ID}";
                        else
                            request = $"{request}&selected-cpu={SelectedCpu.ID}";
                    }
                    if (SelectedHdd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-hdd={SelectedHdd.ID}";
                        else
                            request = $"{request}&selected-hdd={SelectedHdd.ID}";
                    }
                    if (SelectedMotherboard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-motherboard={SelectedMotherboard.ID}";
                        else
                            request = $"{request}&selected-motherboard={SelectedMotherboard.ID}";
                    }
                    if (SelectedRam != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ram={SelectedRam.ID}";
                        else
                            request = $"{request}&selected-ram={SelectedRam.ID}";
                    }
                    if (SelectedSsd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ssd={SelectedSsd.ID}";
                        else
                            request = $"{request}&selected-ssd={SelectedSsd.ID}";
                    }
                    if (SelectedVideocard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-videocard={SelectedVideocard.ID}";
                        else
                            request = $"{request}&selected-videocard={SelectedVideocard.ID}";
                    }

                    HttpWebRequest get = (HttpWebRequest)WebRequest.Create(request);

                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)await get.GetResponseAsync();
                    }
                    catch (WebException)
                    {
                        IsFaulted = true;
                        OnPropertyChanged("IsFaulted");
                        return;
                    }
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            Chargers?.Clear();
                            string res = await reader.ReadToEndAsync();
                            foreach (var e in JsonConvert.DeserializeObject<IEnumerable<Charger>>(res))
                            {
                                if (Chargers != null && !Chargers.Contains(e))
                                {
                                    Chargers.Add(e);
                                }
                            }
                            OnPropertyChanged("Chargers");
                        }
                    }
                }
                break;
                case "cooler":
                {
                    if (_selectedCoolerFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedCoolerFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

                    if (SelectedBody != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-body={SelectedBody.ID}";
                        else
                            request = $"{request}&selected-body={SelectedBody.ID}";
                    }
                    if (SelectedCharger != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-charger={SelectedCharger.ID}";
                        else
                            request = $"{request}&selected-charger={SelectedCharger.ID}";
                    }
                    if (SelectedCooler != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cooler={SelectedCooler.ID}";
                        else
                            request = $"{request}&selected-cooler={SelectedCooler.ID}";
                    }
                    if (SelectedCpu != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cpu={SelectedCpu.ID}";
                        else
                            request = $"{request}&selected-cpu={SelectedCpu.ID}";
                    }
                    if (SelectedHdd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-hdd={SelectedHdd.ID}";
                        else
                            request = $"{request}&selected-hdd={SelectedHdd.ID}";
                    }
                    if (SelectedMotherboard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-motherboard={SelectedMotherboard.ID}";
                        else
                            request = $"{request}&selected-motherboard={SelectedMotherboard.ID}";
                    }
                    if (SelectedRam != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ram={SelectedRam.ID}";
                        else
                            request = $"{request}&selected-ram={SelectedRam.ID}";
                    }
                    if (SelectedSsd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ssd={SelectedSsd.ID}";
                        else
                            request = $"{request}&selected-ssd={SelectedSsd.ID}";
                    }
                    if (SelectedVideocard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-videocard={SelectedVideocard.ID}";
                        else
                            request = $"{request}&selected-videocard={SelectedVideocard.ID}";
                    }

                    HttpWebRequest get = (HttpWebRequest)WebRequest.Create(request);

                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)await get.GetResponseAsync();
                    }
                    catch (WebException)
                    {
                        IsFaulted = true;
                        OnPropertyChanged("IsFaulted");
                        return;
                    }
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            Coolers?.Clear();
                            string res = await reader.ReadToEndAsync();
                            foreach (var e in JsonConvert.DeserializeObject<IEnumerable<Cooler>>(res))
                            {
                                if (Coolers != null && !Coolers.Contains(e))
                                {
                                    Coolers.Add(e);
                                }
                            }
                            OnPropertyChanged("Coolers");
                        }
                    }
                }
                break;
                case "cpu":
                {
                    if (_selectedCpuFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedCpuFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

                    if (SelectedBody != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-body={SelectedBody.ID}";
                        else
                            request = $"{request}&selected-body={SelectedBody.ID}";
                    }
                    if (SelectedCharger != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-charger={SelectedCharger.ID}";
                        else
                            request = $"{request}&selected-charger={SelectedCharger.ID}";
                    }
                    if (SelectedCooler != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cooler={SelectedCooler.ID}";
                        else
                            request = $"{request}&selected-cooler={SelectedCooler.ID}";
                    }
                    if (SelectedCpu != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cpu={SelectedCpu.ID}";
                        else
                            request = $"{request}&selected-cpu={SelectedCpu.ID}";
                    }
                    if (SelectedHdd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-hdd={SelectedHdd.ID}";
                        else
                            request = $"{request}&selected-hdd={SelectedHdd.ID}";
                    }
                    if (SelectedMotherboard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-motherboard={SelectedMotherboard.ID}";
                        else
                            request = $"{request}&selected-motherboard={SelectedMotherboard.ID}";
                    }
                    if (SelectedRam != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ram={SelectedRam.ID}";
                        else
                            request = $"{request}&selected-ram={SelectedRam.ID}";
                    }
                    if (SelectedSsd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ssd={SelectedSsd.ID}";
                        else
                            request = $"{request}&selected-ssd={SelectedSsd.ID}";
                    }
                    if (SelectedVideocard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-videocard={SelectedVideocard.ID}";
                        else
                            request = $"{request}&selected-videocard={SelectedVideocard.ID}";
                    }

                    HttpWebRequest get = (HttpWebRequest)WebRequest.Create(request);

                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)await get.GetResponseAsync();
                    }
                    catch (WebException)
                    {
                        IsFaulted = true;
                        OnPropertyChanged("IsFaulted");
                        return;
                    }
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            Cpus?.Clear();
                            string res = await reader.ReadToEndAsync();
                            foreach (var e in JsonConvert.DeserializeObject<IEnumerable<CPU>>(res))
                            {
                                if (Cpus != null && !Cpus.Contains(e))
                                {
                                    Cpus.Add(e);
                                }
                            }
                            OnPropertyChanged("Cpus");
                        }
                    }
                }
                break;
                case "hdd":
                {
                    if (_selectedHddFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedHddFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

                    if (SelectedBody != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-body={SelectedBody.ID}";
                        else
                            request = $"{request}&selected-body={SelectedBody.ID}";
                    }
                    if (SelectedCharger != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-charger={SelectedCharger.ID}";
                        else
                            request = $"{request}&selected-charger={SelectedCharger.ID}";
                    }
                    if (SelectedCooler != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cooler={SelectedCooler.ID}";
                        else
                            request = $"{request}&selected-cooler={SelectedCooler.ID}";
                    }
                    if (SelectedCpu != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cpu={SelectedCpu.ID}";
                        else
                            request = $"{request}&selected-cpu={SelectedCpu.ID}";
                    }
                    if (SelectedHdd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-hdd={SelectedHdd.ID}";
                        else
                            request = $"{request}&selected-hdd={SelectedHdd.ID}";
                    }
                    if (SelectedMotherboard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-motherboard={SelectedMotherboard.ID}";
                        else
                            request = $"{request}&selected-motherboard={SelectedMotherboard.ID}";
                    }
                    if (SelectedRam != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ram={SelectedRam.ID}";
                        else
                            request = $"{request}&selected-ram={SelectedRam.ID}";
                    }
                    if (SelectedSsd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ssd={SelectedSsd.ID}";
                        else
                            request = $"{request}&selected-ssd={SelectedSsd.ID}";
                    }
                    if (SelectedVideocard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-videocard={SelectedVideocard.ID}";
                        else
                            request = $"{request}&selected-videocard={SelectedVideocard.ID}";
                    }

                    HttpWebRequest get = (HttpWebRequest)WebRequest.Create(request);

                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)await get.GetResponseAsync();
                    }
                    catch (WebException)
                    {
                        IsFaulted = true;
                        OnPropertyChanged("IsFaulted");
                        return;
                    }
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            Hdds?.Clear();
                            string res = await reader.ReadToEndAsync();
                            foreach (var e in JsonConvert.DeserializeObject<IEnumerable<HDD>>(res))
                            {
                                if (Hdds != null && !Hdds.Contains(e))
                                {
                                    Hdds.Add(e);
                                }
                            }
                            OnPropertyChanged("Hdds");
                        }
                    }
                }
                break;
                case "motherboard":
                {
                    if (_selectedMotherboardFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedMotherboardFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

                    if (SelectedBody != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-body={SelectedBody.ID}";
                        else
                            request = $"{request}&selected-body={SelectedBody.ID}";
                    }
                    if (SelectedCharger != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-charger={SelectedCharger.ID}";
                        else
                            request = $"{request}&selected-charger={SelectedCharger.ID}";
                    }
                    if (SelectedCooler != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cooler={SelectedCooler.ID}";
                        else
                            request = $"{request}&selected-cooler={SelectedCooler.ID}";
                    }
                    if (SelectedCpu != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cpu={SelectedCpu.ID}";
                        else
                            request = $"{request}&selected-cpu={SelectedCpu.ID}";
                    }
                    if (SelectedHdd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-hdd={SelectedHdd.ID}";
                        else
                            request = $"{request}&selected-hdd={SelectedHdd.ID}";
                    }
                    if (SelectedMotherboard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-motherboard={SelectedMotherboard.ID}";
                        else
                            request = $"{request}&selected-motherboard={SelectedMotherboard.ID}";
                    }
                    if (SelectedRam != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ram={SelectedRam.ID}";
                        else
                            request = $"{request}&selected-ram={SelectedRam.ID}";
                    }
                    if (SelectedSsd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ssd={SelectedSsd.ID}";
                        else
                            request = $"{request}&selected-ssd={SelectedSsd.ID}";
                    }
                    if (SelectedVideocard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-videocard={SelectedVideocard.ID}";
                        else
                            request = $"{request}&selected-videocard={SelectedVideocard.ID}";
                    }

                    HttpWebRequest get = (HttpWebRequest)WebRequest.Create(request);

                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)await get.GetResponseAsync();
                    }
                    catch (WebException)
                    {
                        IsFaulted = true;
                        OnPropertyChanged("IsFaulted");
                        return;
                    }
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            Motherboards?.Clear();
                            string res = await reader.ReadToEndAsync();
                            foreach (var e in JsonConvert.DeserializeObject<IEnumerable<Motherboard>>(res))
                            {
                                if (Motherboards != null && !Motherboards.Contains(e))
                                {
                                    Motherboards.Add(e);
                                }
                            }
                            OnPropertyChanged("Motherboards");
                        }
                    }
                }
                break;
                case "ram":
                {
                    if (_selectedRamFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedRamFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

                    if (SelectedBody != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-body={SelectedBody.ID}";
                        else
                            request = $"{request}&selected-body={SelectedBody.ID}";
                    }
                    if (SelectedCharger != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-charger={SelectedCharger.ID}";
                        else
                            request = $"{request}&selected-charger={SelectedCharger.ID}";
                    }
                    if (SelectedCooler != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cooler={SelectedCooler.ID}";
                        else
                            request = $"{request}&selected-cooler={SelectedCooler.ID}";
                    }
                    if (SelectedCpu != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cpu={SelectedCpu.ID}";
                        else
                            request = $"{request}&selected-cpu={SelectedCpu.ID}";
                    }
                    if (SelectedHdd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-hdd={SelectedHdd.ID}";
                        else
                            request = $"{request}&selected-hdd={SelectedHdd.ID}";
                    }
                    if (SelectedMotherboard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-motherboard={SelectedMotherboard.ID}";
                        else
                            request = $"{request}&selected-motherboard={SelectedMotherboard.ID}";
                    }
                    if (SelectedRam != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ram={SelectedRam.ID}";
                        else
                            request = $"{request}&selected-ram={SelectedRam.ID}";
                    }
                    if (SelectedSsd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ssd={SelectedSsd.ID}";
                        else
                            request = $"{request}&selected-ssd={SelectedSsd.ID}";
                    }
                    if (SelectedVideocard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-videocard={SelectedVideocard.ID}";
                        else
                            request = $"{request}&selected-videocard={SelectedVideocard.ID}";
                    }

                    HttpWebRequest get = (HttpWebRequest)WebRequest.Create(request);

                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)await get.GetResponseAsync();
                    }
                    catch (WebException)
                    {
                        IsFaulted = true;
                        OnPropertyChanged("IsFaulted");
                        return;
                    }
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            Rams?.Clear();
                            string res = await reader.ReadToEndAsync();
                            foreach (var e in JsonConvert.DeserializeObject<IEnumerable<RAM>>(res))
                            {
                                if (Rams != null && !Rams.Contains(e))
                                {
                                    Rams.Add(e);
                                }
                            }
                            OnPropertyChanged("Rams");
                        }
                    }
                }
                break;
                case "ssd":
                {
                    if (_selectedSsdFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedSsdFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

                    if (SelectedBody != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-body={SelectedBody.ID}";
                        else
                            request = $"{request}&selected-body={SelectedBody.ID}";
                    }
                    if (SelectedCharger != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-charger={SelectedCharger.ID}";
                        else
                            request = $"{request}&selected-charger={SelectedCharger.ID}";
                    }
                    if (SelectedCooler != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cooler={SelectedCooler.ID}";
                        else
                            request = $"{request}&selected-cooler={SelectedCooler.ID}";
                    }
                    if (SelectedCpu != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cpu={SelectedCpu.ID}";
                        else
                            request = $"{request}&selected-cpu={SelectedCpu.ID}";
                    }
                    if (SelectedHdd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-hdd={SelectedHdd.ID}";
                        else
                            request = $"{request}&selected-hdd={SelectedHdd.ID}";
                    }
                    if (SelectedMotherboard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-motherboard={SelectedMotherboard.ID}";
                        else
                            request = $"{request}&selected-motherboard={SelectedMotherboard.ID}";
                    }
                    if (SelectedRam != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ram={SelectedRam.ID}";
                        else
                            request = $"{request}&selected-ram={SelectedRam.ID}";
                    }
                    if (SelectedSsd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ssd={SelectedSsd.ID}";
                        else
                            request = $"{request}&selected-ssd={SelectedSsd.ID}";
                    }
                    if (SelectedVideocard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-videocard={SelectedVideocard.ID}";
                        else
                            request = $"{request}&selected-videocard={SelectedVideocard.ID}";
                    }

                    HttpWebRequest get = (HttpWebRequest)WebRequest.Create(request);

                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)await get.GetResponseAsync();
                    }
                    catch (WebException)
                    {
                        IsFaulted = true;
                        OnPropertyChanged("IsFaulted");
                        return;
                    }
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            Ssds?.Clear();
                            string res = await reader.ReadToEndAsync();
                            foreach (var e in JsonConvert.DeserializeObject<IEnumerable<SSD>>(res))
                            {
                                if (Ssds != null && !Ssds.Contains(e))
                                {
                                    Ssds.Add(e);
                                }
                            }
                            OnPropertyChanged("Ssds");
                        }
                    }
                }
                break;
                case "videocard":
                {
                    if (_selectedVideocardFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedVideocardFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

                    if (SelectedBody != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-body={SelectedBody.ID}";
                        else
                            request = $"{request}&selected-body={SelectedBody.ID}";
                    }
                    if (SelectedCharger != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-charger={SelectedCharger.ID}";
                        else
                            request = $"{request}&selected-charger={SelectedCharger.ID}";
                    }
                    if (SelectedCooler != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cooler={SelectedCooler.ID}";
                        else
                            request = $"{request}&selected-cooler={SelectedCooler.ID}";
                    }
                    if (SelectedCpu != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-cpu={SelectedCpu.ID}";
                        else
                            request = $"{request}&selected-cpu={SelectedCpu.ID}";
                    }
                    if (SelectedHdd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-hdd={SelectedHdd.ID}";
                        else
                            request = $"{request}&selected-hdd={SelectedHdd.ID}";
                    }
                    if (SelectedMotherboard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-motherboard={SelectedMotherboard.ID}";
                        else
                            request = $"{request}&selected-motherboard={SelectedMotherboard.ID}";
                    }
                    if (SelectedRam != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ram={SelectedRam.ID}";
                        else
                            request = $"{request}&selected-ram={SelectedRam.ID}";
                    }
                    if (SelectedSsd != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-ssd={SelectedSsd.ID}";
                        else
                            request = $"{request}&selected-ssd={SelectedSsd.ID}";
                    }
                    if (SelectedVideocard != null)
                    {
                        if (request.Last() == '?')
                            request = $"{request}selected-videocard={SelectedVideocard.ID}";
                        else
                            request = $"{request}&selected-videocard={SelectedVideocard.ID}";
                    }

                    HttpWebRequest get = (HttpWebRequest)WebRequest.Create(request);

                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)await get.GetResponseAsync();
                    }
                    catch (WebException)
                    {
                        IsFaulted = true;
                        OnPropertyChanged("IsFaulted");
                        return;
                    }
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            Videocards?.Clear();
                            string res = await reader.ReadToEndAsync();
                            foreach (var e in JsonConvert.DeserializeObject<IEnumerable<Videocard>>(res))
                            {
                                if (Videocards != null && !Videocards.Contains(e))
                                {
                                    Videocards.Add(e);
                                }
                            }
                            OnPropertyChanged("Videocards");
                        }
                    }
                }
                break;
            }
        }

        public void Clear(string type)
        {
            switch (type)
            {
                case "body": _selectedBodyFields.Clear(); break;
                case "charger": _selectedChargerFields.Clear(); break;
                case "cooler": _selectedCoolerFields.Clear(); break;
                case "cpu": _selectedCpuFields.Clear(); break;
                case "hdd": _selectedHddFields.Clear(); break;
                case "motherboard": _selectedMotherboardFields.Clear(); break;
                case "ram": _selectedRamFields.Clear(); break;
                case "ssd": _selectedSsdFields.Clear(); break;
                case "videocard": _selectedVideocardFields.Clear(); break;
            }
        }

        public void SelectItem(string type, int id)
        {
            switch (type)
            {
                case "body":
                {
                    SelectedBody = Bodies.Where((o) => o.ID == id).ToList()?.First();
                }
                break;
                case "charger":
                {
                    SelectedCharger = Chargers.Where((o) => o.ID == id).ToList()?.First();
                }
                break;
                case "cooler":
                {
                    SelectedCooler = Coolers.Where((o) => o.ID == id).ToList()?.First();
                }
                break;
                case "cpu":
                {
                    SelectedCpu = Cpus.Where((o) => o.ID == id).ToList()?.First();
                }
                break;
                case "hdd":
                {
                    SelectedHdd = Hdds.Where((o) => o.ID == id).ToList()?.First();
                }
                break;
                case "motherboard":
                {
                    SelectedMotherboard = Motherboards.Where((o) => o.ID == id).ToList()?.First();
                }
                break;
                case "ram":
                {
                    SelectedRam = Rams.Where((o) => o.ID == id).ToList()?.First();
                }
                break;
                case "ssd":
                {
                    SelectedSsd = Ssds.Where((o) => o.ID == id).ToList()?.First();
                }
                break;
                case "videocard":
                {
                    SelectedVideocard = Videocards.Where((o) => o.ID == id).ToList()?.First();
                }
                break;
            }
        }

        public async void ClearSelectedItem(string type)
        {
            switch (type)
            {
                case "body":
                {
                    _selectedBody = null;
                    OnPropertyChanged("SelectedBody");
                }
                break;
                case "charger":
                {
                    _selectedCharger = null;
                    OnPropertyChanged("SelectedCharger");
                }
                break;
                case "cooler":
                {
                    _selectedCooler = null;
                    OnPropertyChanged("SelectedCooler");
                }
                break;
                case "cpu":
                {
                    _selectedCpu = null;
                    OnPropertyChanged("SelectedCpu");
                }
                break;
                case "hdd":
                {
                    _selectedHdd = null;
                    OnPropertyChanged("SelectedHdd");
                }
                break;
                case "motherboard":
                {
                    _selectedMotherboard = null;
                    OnPropertyChanged("SelectedMotherboard");
                }
                break;
                case "ram":
                {
                    _selectedRam = null;
                    OnPropertyChanged("SelectedRam");
                }
                break;
                case "ssd":
                {
                    _selectedSsd = null;
                    OnPropertyChanged("SelectedSsd");
                }
                break;
                case "videocard":
                {
                    _selectedVideocard = null;
                    OnPropertyChanged("SelectedVideocard");
                }
                break;
            }
            await UpdateAllData();
        }

        public async void SaveSelection(string path, string format)
        {
            switch (format.ToLower().Trim())
            {
                case "xml":
                {
                    using (StreamWriter writer = new StreamWriter(File.Create(path)))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Selected));
                        var selected = new Selected()
                        {
                            SelectedBody = SelectedBody,
                            SelectedCharger = SelectedCharger,
                            SelectedCooler = SelectedCooler,
                            SelectedCPU = SelectedCpu,
                            SelectedHDD = SelectedHdd,
                            SelectedMotherboard = SelectedMotherboard,
                            SelectedRAM = SelectedRam,
                            SelectedSSD = SelectedSsd,
                            SelectedVideocard = SelectedVideocard
                        };
                        serializer.Serialize(writer, selected);
                    }
                }
                break;
                case "json":
                {
                    using (StreamWriter writer = new StreamWriter(File.Create(path)))
                    {
                        var selected = new Selected()
                        {
                            SelectedBody = SelectedBody,
                            SelectedCharger = SelectedCharger,
                            SelectedCooler = SelectedCooler,
                            SelectedCPU = SelectedCpu,
                            SelectedHDD = SelectedHdd,
                            SelectedMotherboard = SelectedMotherboard,
                            SelectedRAM = SelectedRam,
                            SelectedSSD = SelectedSsd,
                            SelectedVideocard = SelectedVideocard
                        };
                        string json = JsonConvert.SerializeObject(selected, Newtonsoft.Json.Formatting.Indented);
                        await writer.WriteLineAsync(json);
                    }
                }
                break;
                default:
                {

                }
                break;
            }
        }

        public void OpenSelection(string path, string format)
        {
            switch (format)
            {
                case "xml":
                {
                    using (StreamReader reader = new StreamReader(File.OpenRead(path)))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Selected));
                        var info = (Selected)serializer.Deserialize(reader);
                        SelectedBody = info.SelectedBody;
                        OnPropertyChanged("SelectedBody");
                        SelectedCharger = info.SelectedCharger;
                        OnPropertyChanged("SelectedCharger");
                        SelectedCooler = info.SelectedCooler;
                        OnPropertyChanged("SelectedCooler");
                        SelectedCpu = info.SelectedCPU;
                        OnPropertyChanged("SelectedCpu");
                        SelectedHdd = info.SelectedHDD;
                        OnPropertyChanged("SelectedHdd");
                        SelectedMotherboard = info.SelectedMotherboard;
                        OnPropertyChanged("SelectedMotherboard");
                        SelectedRam = info.SelectedRAM;
                        OnPropertyChanged("SelectedRam");
                        SelectedSsd = info.SelectedSSD;
                        OnPropertyChanged("SelectedSsd");
                        SelectedVideocard = info.SelectedVideocard;
                        OnPropertyChanged("SelectedVideocard");
                    }
                }
                break;
                case "json":
                {
                    using (StreamReader reader = new StreamReader(File.OpenRead(path)))
                    {
                        string json = reader.ReadToEnd().Trim();
                        var info = JsonConvert.DeserializeObject<Selected>(json);
                        SelectedBody = info.SelectedBody;
                        OnPropertyChanged("SelectedBody");
                        SelectedCharger = info.SelectedCharger;
                        OnPropertyChanged("SelectedCharger");
                        SelectedCooler = info.SelectedCooler;
                        OnPropertyChanged("SelectedCooler");
                        SelectedCpu = info.SelectedCPU;
                        OnPropertyChanged("SelectedCpu");
                        SelectedHdd = info.SelectedHDD;
                        OnPropertyChanged("SelectedHdd");
                        SelectedMotherboard = info.SelectedMotherboard;
                        OnPropertyChanged("SelectedMotherboard");
                        SelectedRam = info.SelectedRAM;
                        OnPropertyChanged("SelectedRam");
                        SelectedSsd = info.SelectedSSD;
                        OnPropertyChanged("SelectedSsd");
                        SelectedVideocard = info.SelectedVideocard;
                        OnPropertyChanged("SelectedVideocard");
                    }
                }
                break;
                default:
                {

                }
                break;
            }
        }

        public async void ChangeCulture(string newCulture)
        {
            if (newCulture != null)
            {
                _culture = newCulture;
                await Initialize();
            }
        }
    }
}
