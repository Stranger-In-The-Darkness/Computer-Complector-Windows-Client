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
		/// <summary>
		/// API uri
		/// </summary>
        private string _apiUri = null;
#endif
		/// <summary>
		/// App culture
		/// </summary>
        private string _culture = string.Empty;
        /// <summary>
        /// True, if web request is faulted
        /// </summary>
        public bool IsFaulted { get; private set; } = false;

		/// <summary>
		/// List of bodies
		/// </summary>
        private List<Body> _bodies;
		/// <summary>
		/// Selected body
		/// </summary>
        private Body _selectedBody;
		/// <summary>
		/// Filters for bodies
		/// </summary>
        private Dictionary<string, Option> _bodyFields;
		/// <summary>
		/// Selected bodies filters values
		/// </summary>
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

		/// <summary>
		/// Creates new model
		/// </summary>
		/// <param name="apiUri">APU uri</param>
		/// <param name="culture">Culture code</param>
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
			try
			{
				switch (type)
				{
					case "body":
					{
						string request = $"{_apiUri}/bodies";

						Bodies = (await SendHTTPRequest<IEnumerable<Body>>("GET", request)).ToList();
						OnPropertyChanged("Bodies");
					}
					break;
					case "charger":
					{
						string request = $"{_apiUri}/{type}s";

						Chargers = (await SendHTTPRequest<IEnumerable<Charger>>("GET", request)).ToList();
						OnPropertyChanged("Chargers");
					}
					break;
					case "cooler":
					{
						string request = $"{_apiUri}/{type}s";

						Coolers = (await SendHTTPRequest<IEnumerable<Cooler>>("GET", request)).ToList();
						OnPropertyChanged("Coolers");
					}
					break;
					case "cpu":
					{
						string request = $"{_apiUri}/{type}s";

						Cpus = (await SendHTTPRequest<IEnumerable<CPU>>("GET", request)).ToList();
						OnPropertyChanged("Cpus");
					}
					break;
					case "hdd":
					{
						string request = $"{_apiUri}/{type}s";

						Hdds = (await SendHTTPRequest<IEnumerable<HDD>>("GET", request)).ToList();
						OnPropertyChanged("Hdds");
					}
					break;
					case "motherboard":
					{
						string request = $"{_apiUri}/{type}s";

						Motherboards = (await SendHTTPRequest<IEnumerable<Motherboard>>("GET", request)).ToList();
						OnPropertyChanged("Motherboards");
					}
					break;
					case "ram":
					{
						string request = $"{_apiUri}/{type}s";

						Rams = (await SendHTTPRequest<IEnumerable<RAM>>("GET", request)).ToList();
						OnPropertyChanged("Rams");
					}
					break;
					case "ssd":
					{
						string request = $"{_apiUri}/{type}s";

						Ssds = (await SendHTTPRequest<IEnumerable<SSD>>("GET", request)).ToList();
						OnPropertyChanged("Ssds");
					}
					break;
					case "videocard":
					{
						string request = $"{_apiUri}/{type}s";

						Videocards = (await SendHTTPRequest<IEnumerable<Videocard>>("GET", request)).ToList();
						OnPropertyChanged("Videocards");
						break;
					}
				}
			}
			catch (WebException)
			{
				IsFaulted = true;
				OnPropertyChanged("IsFaulted");
				return;
			}
        }

        /// <summary>
        /// Loads all filters form specified component
        /// </summary>
        /// <param name="type">component name</param>
        /// <returns></returns>
        public async Task LoadFields(string type)
        {
			try
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

					var v = await SendHTTPRequest<Dictionary<string, Option>>("GET", request);

					switch (type)
					{
						case "body":
							BodyFields = v;
							OnPropertyChanged("BodyFields");
							break;
						case "charger":
							ChargerFields = v;
							OnPropertyChanged("ChargerFields");
							break;
						case "cooler":
							CoolerFields = v;
							OnPropertyChanged("CoolerFields");
							break;
						case "cpu":
							CPUFields = v;
							OnPropertyChanged("CPUFields");
							break;
						case "hdd":
							HDDFields = v;
							OnPropertyChanged("HDDFields");
							break;
						case "motherboard":
							MotherboardFields = v;
							OnPropertyChanged("MotherboardFields");
							break;
						case "ram":
							RAMFields = v;
							OnPropertyChanged("RAMFields");
							break;
						case "ssd":
							SSDFields = v;
							OnPropertyChanged("SSDFields");
							break;
						case "videocard":
							VideocardFields = v;
							OnPropertyChanged("VideocardFields");
							break;
					}
				}
			}
			catch (WebException)
			{
				IsFaulted = true;
				OnPropertyChanged("IsFaulted");
				return;
			}
        }

		/// <summary>
		/// Adds value to selected filters if not yet selected, otherwise deletes it from selected
		/// </summary>
		/// <param name="type">Component</param>
		/// <param name="property">Filter name</param>
		/// <param name="value">Target value</param>
		/// <returns></returns>
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

		/// <summary>
		/// Reloads all of the records
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Reloads only records of selected component
		/// </summary>
		/// <param name="type">Component</param>
		/// <returns></returns>
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

					request = AddSelectedToRequest(request);

					try
					{
						Bodies = (await SendHTTPRequest<IEnumerable<Body>>("GET", request)).ToList();
						OnPropertyChanged("Bodies");
					}
					catch (WebException)
					{
						IsFaulted = true;
						OnPropertyChanged("IsFaulted");
						return;
					}
                }
                break;
                case "charger":
                {
                    if (_selectedChargerFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedChargerFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

					request = AddSelectedToRequest(request);

					try
					{
						Chargers = (await SendHTTPRequest<IEnumerable<Charger>>("GET", request)).ToList();
						OnPropertyChanged("Chargers");
					}
					catch (WebException)
					{
						IsFaulted = true;
						OnPropertyChanged("IsFaulted");
						return;
					}
				}
                break;
                case "cooler":
                {
                    if (_selectedCoolerFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedCoolerFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

					request = AddSelectedToRequest(request);

					try
					{
						Coolers = (await SendHTTPRequest<IEnumerable<Cooler>>("GET", request)).ToList();
						OnPropertyChanged("Coolers");
					}
					catch (WebException)
					{
						IsFaulted = true;
						OnPropertyChanged("IsFaulted");
						return;
					}
				}
                break;
                case "cpu":
                {
                    if (_selectedCpuFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedCpuFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

					request = AddSelectedToRequest(request);

					try
					{
						Cpus = (await SendHTTPRequest<IEnumerable<CPU>>("GET", request)).ToList();
						OnPropertyChanged("Cpus");
					}
					catch (WebException)
					{
						IsFaulted = true;
						OnPropertyChanged("IsFaulted");
						return;
					}
				}
                break;
                case "hdd":
                {
                    if (_selectedHddFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedHddFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

					request = AddSelectedToRequest(request);

					try
					{
						Hdds = (await SendHTTPRequest<IEnumerable<HDD>>("GET", request)).ToList();
						OnPropertyChanged("Hdds");
					}
					catch (WebException)
					{
						IsFaulted = true;
						OnPropertyChanged("IsFaulted");
						return;
					}
				}
                break;
                case "motherboard":
                {
                    if (_selectedMotherboardFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedMotherboardFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

					request = AddSelectedToRequest(request);

					try
					{
						Motherboards = (await SendHTTPRequest<IEnumerable<Motherboard>>("GET", request)).ToList();
						OnPropertyChanged("Motherboards");
					}
					catch (WebException)
					{
						IsFaulted = true;
						OnPropertyChanged("IsFaulted");
						return;
					}
				}
                break;
                case "ram":
                {
                    if (_selectedRamFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedRamFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

					request = AddSelectedToRequest(request);

					try
					{
						Rams = (await SendHTTPRequest<IEnumerable<RAM>>("GET", request)).ToList();
						OnPropertyChanged("Rams");
					}
					catch (WebException)
					{
						IsFaulted = true;
						OnPropertyChanged("IsFaulted");
						return;
					}
				}
                break;
                case "ssd":
                {
                    if (_selectedSsdFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedSsdFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

					request = AddSelectedToRequest(request);

					try
					{
						Ssds = (await SendHTTPRequest<IEnumerable<SSD>>("GET", request)).ToList();
						OnPropertyChanged("Ssds");
					}
					catch (WebException)
					{
						IsFaulted = true;
						OnPropertyChanged("IsFaulted");
						return;
					}
				}
                break;
                case "videocard":
                {
                    if (_selectedVideocardFields.Count > 0)
                    {
                        request = $"{request}{string.Join("&", _selectedVideocardFields.Select((kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))))}";
                    }

					request = AddSelectedToRequest(request);

					try
					{
						Videocards = (await SendHTTPRequest<IEnumerable<Videocard>>("GET", request)).ToList();
						OnPropertyChanged("Videocards");
					}
					catch (WebException)
					{
						IsFaulted = true;
						OnPropertyChanged("IsFaulted");
						return;
					}
				}
                break;
            }
        }

		/// <summary>
		/// Clears all of the applied filters on component
		/// </summary>
		/// <param name="type">Component</param>
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

		/// <summary>
		/// Adds component to the selection
		/// </summary>
		/// <param name="type">Component</param>
		/// <param name="id">Element ID</param>
        public void SelectItem(string type, int id)
        {
            switch (type)
            {
                case "body":
                {
                    SelectedBody = Bodies.FirstOrDefault((o) => o.ID == id);
                }
                break;
                case "charger":
                {
                    SelectedCharger = Chargers.FirstOrDefault((o) => o.ID == id);
				}
                break;
                case "cooler":
                {
                    SelectedCooler = Coolers.FirstOrDefault((o) => o.ID == id);
				}
                break;
                case "cpu":
                {
                    SelectedCpu = Cpus.FirstOrDefault((o) => o.ID == id);
				}
                break;
                case "hdd":
                {
                    SelectedHdd = Hdds.FirstOrDefault((o) => o.ID == id);
				}
                break;
                case "motherboard":
                {
                    SelectedMotherboard = Motherboards.FirstOrDefault((o) => o.ID == id);
				}
                break;
                case "ram":
                {
                    SelectedRam = Rams.FirstOrDefault((o) => o.ID == id);
				}
                break;
                case "ssd":
                {
                    SelectedSsd = Ssds.FirstOrDefault((o) => o.ID == id);
				}
                break;
                case "videocard":
                {
                    SelectedVideocard = Videocards.FirstOrDefault((o) => o.ID == id);
				}
                break;
            }
        }

		/// <summary>
		/// Deletes selection of component
		/// </summary>
		/// <param name="type">Component</param>
		/// <returns></returns>
        public async Task ClearSelectedItem(string type)
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

		/// <summary>
		/// Saves selection to file
		/// </summary>
		/// <param name="path">File path</param>
		/// <param name="format">File format</param>
		/// <returns></returns>
        public async Task SaveSelection(string path, string format)
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

		/// <summary>
		/// Loads selection from file
		/// </summary>
		/// <param name="path">File path</param>
		/// <param name="format">File format</param>
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

		/// <summary>
		/// Changes culture of the application
		/// </summary>
		/// <param name="newCulture">New culture</param>
		/// <returns></returns>
        public async Task ChangeCulture(string newCulture)
        {
            if (newCulture != null)
            {
                _culture = newCulture;
                await Initialize();
            }
        }

		/// <summary>
		/// Adds component element
		/// </summary>
		/// <param name="type">Component</param>
		/// <param name="model">Element to add</param>
		/// <returns></returns>
		public async Task AddModel(string type, object model, string token)
		{
			switch (type)
			{
				case "body":
				{
					var m = model as Body;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<Body>>("POST", $"{_apiUri}/{type}", content, token);

						Bodies = res.ToList();
						OnPropertyChanged("Bodies");
					}
				}
				break;
				case "charger":
				{
					var m = model as Charger;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<Charger>>("POST", $"{_apiUri}/{type}", content, token);

						Chargers = res.ToList();
						OnPropertyChanged("Chargers");
					}
				}
				break;
				case "cooler":
				{
					var m = model as Cooler;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<Cooler>>("POST", $"{_apiUri}/{type}", content, token);

						Coolers = res.ToList();
						OnPropertyChanged("Coolers");
					}
				}
				break;
				case "cpu":
				{
					var m = model as CPU;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<CPU>>("POST", $"{_apiUri}/{type}", content, token);

						Cpus = res.ToList();
						OnPropertyChanged("Cpus");
					}
				}
				break;
				case "hdd":
				{
					var m = model as HDD;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<HDD>>("POST", $"{_apiUri}/{type}", content, token);

						Hdds = res.ToList();
						OnPropertyChanged("Hdds");
					}
				}
				break;
				case "motherboard":
				{
					var m = model as Motherboard;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<Motherboard>>("POST", $"{_apiUri}/{type}", content, token);

						Motherboards = res.ToList();
						OnPropertyChanged("Motherboards");
					}
				}
				break;
				case "ram":
				{
					var m = model as RAM;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<RAM>>("POST", $"{_apiUri}/{type}", content, token);

						Rams = res.ToList();
						OnPropertyChanged("Rams");
					}
				}
				break;
				case "ssd":
				{
					var m = model as SSD;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<SSD>>("POST", $"{_apiUri}/{type}", content, token);

						Ssds = res.ToList();
						OnPropertyChanged("Ssds");
					}
				}
				break;
				case "videocard":
				{
					var m = model as Videocard;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<Videocard>>("POST", $"{_apiUri}/{type}", content, token);

						Videocards = res.ToList();
						OnPropertyChanged("Videocards");
					}
				}
				break;
			}
		}

		/// <summary>
		/// Replaces component element with specified ID
		/// </summary>
		/// <param name="type">Component</param>
		/// <param name="id">Element ID</param>
		/// <param name="model">New element</param>
		/// <returns></returns>
		public async Task ReplaceModel(string type, int id, object model, string token)
		{
			switch (type)
			{
				case "body":
				{
					var m = model as Body;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<Body>>("PUT", $"{_apiUri}/{type}/{id}", content, token);

						Bodies = res.ToList();
						OnPropertyChanged("Bodies");
					}
				}
				break;
				case "charger":
				{
					var m = model as Charger;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<Charger>>("PUT", $"{_apiUri}/{type}/{id}", content, token);

						Chargers = res.ToList();
						OnPropertyChanged("Chargers");
					}
				}
				break;
				case "cooler":
				{
					var m = model as Cooler;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<Cooler>>("PUT", $"{_apiUri}/{type}/{id}", content, token);

						Coolers = res.ToList();
						OnPropertyChanged("Coolers");
					}
				}
				break;
				case "cpu":
				{
					var m = model as CPU;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<CPU>>("PUT", $"{_apiUri}/{type}/{id}", content, token);

						Cpus = res.ToList();
						OnPropertyChanged("Cpus");
					}
				}
				break;
				case "hdd":
				{
					var m = model as HDD;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<HDD>>("PUT", $"{_apiUri}/{type}/{id}", content, token);

						Hdds = res.ToList();
						OnPropertyChanged("Hdds");
					}
				}
				break;
				case "motherboard":
				{
					var m = model as Motherboard;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<Motherboard>>("PUT", $"{_apiUri}/{type}/{id}", content, token);

						Motherboards = res.ToList();
						OnPropertyChanged("Motherboards");
					}
				}
				break;
				case "ram":
				{
					var m = model as RAM;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<RAM>>("PUT", $"{_apiUri}/{type}/{id}", content, token);

						Rams = res.ToList();
						OnPropertyChanged("Rams");
					}
				}
				break;
				case "ssd":
				{
					var m = model as SSD;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<SSD>>("PUT", $"{_apiUri}/{type}/{id}", content, token);

						Ssds = res.ToList();
						OnPropertyChanged("Ssds");
					}
				}
				break;
				case "videocard":
				{
					var m = model as Videocard;
					if (m != null)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequest<IEnumerable<Videocard>>("PUT", $"{_apiUri}/{type}/{id}", content, token);

						Videocards = res.ToList();
						OnPropertyChanged("Videocards");
					}
				}
				break;
			}
		}

		/// <summary>
		/// Removes component with specified ID
		/// </summary>
		/// <param name="type">Component</param>
		/// <param name="id">Element ID</param>
		/// <returns></returns>
		public async Task DeleteModel(string type, int id, string token)
		{
			switch (type)
			{
				case "body":
				{
					var res = await SendHTTPRequest<IEnumerable<Body>>("DELETE", $"{_apiUri}/{type}/{id}", token: token);

					Bodies = res.ToList();

					OnPropertyChanged("Bodies");
				}
				break;
				case "charger":
				{
					var res = await SendHTTPRequest<IEnumerable<Charger>>("DELETE", $"{_apiUri}/{type}/{id}", token: token);

					Chargers = res.ToList();

					OnPropertyChanged("Chargers");
				}
				break;
				case "cooler":
				{
					var res = await SendHTTPRequest<IEnumerable<Cooler>>("DELETE", $"{_apiUri}/{type}/{id}", token: token);

					Coolers = res.ToList();

					OnPropertyChanged("Coolers");
				}
				break;
				case "cpu":
				{
					var res = await SendHTTPRequest<IEnumerable<CPU>>("DELETE", $"{_apiUri}/{type}/{id}", token: token);

					Cpus = res.ToList();

					OnPropertyChanged("Cpus");
				}
				break;
				case "hdd":
				{
					var res = await SendHTTPRequest<IEnumerable<HDD>>("DELETE", $"{_apiUri}/{type}/{id}", token: token);

					Hdds = res.ToList();

					OnPropertyChanged("Hdds");
				}
				break;
				case "motherboard":
				{
					var res = await SendHTTPRequest<IEnumerable<Motherboard>>("PUT", $"{_apiUri}/{type}/{id}", token: token);

					Motherboards = res.ToList();

					OnPropertyChanged("Motherboards");
				}
				break;
				case "ram":
				{
					var res = await SendHTTPRequest<IEnumerable<RAM>>("DELETE", $"{_apiUri}/{type}/{id}", token: token);

					Rams = res.ToList();

					OnPropertyChanged("Rams");
				}
				break;
				case "ssd":
				{
					var res = await SendHTTPRequest<IEnumerable<SSD>>("PUT", $"{_apiUri}/{type}/{id}", token: token);

					Ssds = res.ToList();

					OnPropertyChanged("Ssds");
				}
				break;
				case "videocard":
				{
					var res = await SendHTTPRequest<IEnumerable<Videocard>>("PUT", $"{_apiUri}/{type}/{id}", token: token);

					Videocards = res.ToList();

					OnPropertyChanged("Videocards");
				}
				break;
			}
		}

		/// <summary>
		/// Sends HTTP request with optional JSON body and authorization token
		/// </summary>
		/// <typeparam name="T">Return value expected type</typeparam>
		/// <param name="method">HTTP method</param>
		/// <param name="path">Request uri</param>
		/// <param name="content">JSON content</param>
		/// <param name="token">Authorization token</param>
		/// <returns></returns>
		private async Task<T> SendHTTPRequest<T>(string method, string path, string content = null, string token = null)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);

			request.Method = method;

			if (token != null)
			{
				var cookies = new CookieContainer();
				cookies.Add(new Cookie("Authorization", $"Bearer {token}"));
				request.CookieContainer = cookies;
				request.ContentType = "application/json";
			}

			if (content != null)
			{
				using (var streamWriter = new StreamWriter(request.GetRequestStream()))
				{
					streamWriter.Write(content);
				}
			}

			HttpWebResponse response = null;
			try
			{
				response = (HttpWebResponse)await request.GetResponseAsync();
			}
			catch (WebException)
			{
				throw;
			}
			catch (Exception e)
			{

			}

			if (response.StatusCode == HttpStatusCode.OK)
			{
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					string res = await reader.ReadToEndAsync();
					try
					{
						return JsonConvert.DeserializeObject<T>(res);
					}
					catch (JsonSerializationException) { throw; };
				}
			}
			else
			{
				return default(T);
			}
		}

		/// <summary>
		/// Adds selected components ID`s to the request
		/// </summary>
		/// <param name="initialRequest">Initial request</param>
		/// <returns></returns>
		private string AddSelectedToRequest(string initialRequest)
		{
			string request = initialRequest;
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
			return request;
		}
    }
}
