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
using Model.Models.Data.Components;
using Model.Models;
using Model.Models.Special;

namespace Model
{
    /// <summary>
    /// MVVM client Model class
    /// </summary>
    public class Model : INotifyPropertyChanged
    {
		private const string GatAllBodiesRequestFormat = "{0}/bodies";
		private const string GetAllElementsRequestFormat = "{0}/{1}s";
		private const string GetPropertiesRequestFormat = "{0}/{1}/properties?culture={2}";
		private const string GetRecommendationsRequestFormat = "{0}/{1}/recommendations?{2}&time-span={3}";
		private const string AddElementRequestFormat = "{0}/{1}";
		private const string ElementsRequestFormat = "{0}/{1}?{2}";
		private const string ElementByIDRequestFormat = "{0}/{1}/{2}";

		/// <summary>
		/// API uri
		/// </summary>
		protected string _apiUri = null;
		protected string _componentsUri = null;
		protected string _statisticsUri = null;
		/// <summary>
		/// App culture
		/// </summary>
        protected string _culture = string.Empty;
        /// <summary>
        /// True, if web request is faulted
        /// </summary>
        public bool IsFaulted { get; protected set; } = false;

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

        public List<CPU> CPUs { get => _cpus; set { _cpus = value; OnPropertyChanged("Cpus"); } }
        public CPU SelectedCPU { get => _selectedCpu; set { if (value != null) { _selectedCpu = value; OnPropertyChanged("SelectedCpu"); } } }
        /// <summary>
        /// Field, description, options
        /// </summary>
        public Dictionary<string, Option> CPUFields { get => _cpuFields; set { _cpuFields = value; OnPropertyChanged("CpuFields"); } }

        public List<HDD> HDDs { get => _hdds; set { _hdds = value; OnPropertyChanged("Hdds"); } }
        public HDD SelectedHDD { get => _selectedHdd; set { if (value != null) { _selectedHdd = value; OnPropertyChanged("SelectedHdd"); } } }
        /// <summary>
        /// Field, description, options
        /// </summary>
        public Dictionary<string, Option> HDDFields { get => _hddFields; set { _hddFields = value; OnPropertyChanged("HddFields"); } }

		public List<Motherboard> Motherboards { get => _motherboards; set { _motherboards = value; OnPropertyChanged("Motherboards"); } }
		/// <summary>
		/// Field, description, options
		/// </summary>
		public Motherboard SelectedMotherboard { get => _selectedMotherboard; set { if (value != null) { _selectedMotherboard = value; OnPropertyChanged("SelectedMotherboard"); } } }
		public Dictionary<string, Option> MotherboardFields { get => _motherboardFields; set { _motherboardFields = value; OnPropertyChanged("MotherboardFields"); } }


		public List<RAM> RAMs { get => _rams; set { _rams = value; OnPropertyChanged("Rams"); } }
        public RAM SelectedRAM { get => _selectedRam; set { if (value != null) { _selectedRam = value; OnPropertyChanged("SelectedRam"); } } }
        /// <summary>
        /// Field, description, options
        /// </summary>
        public Dictionary<string, Option> RAMFields { get => _ramFields; set { _ramFields = value; OnPropertyChanged("RamFields"); } }

        public List<SSD> SSDs { get => _ssds; set { _ssds = value; OnPropertyChanged("Ssds"); } }
        public SSD SelectedSSD { get => _selectedSsd; set { if (value != null) { _selectedSsd = value; OnPropertyChanged("SelectedSsd"); } } }
        /// <summary>
        /// Field, description, options
        /// </summary>
        public Dictionary<string, Option> SSDFields { get => _ssdFields; set { _ssdFields = value; OnPropertyChanged("SsdFields"); } }

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
        public Model(string apiUri, string componentsFormat, string statisticsFormat, string culture = "en")
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

            _apiUri = apiUri;
			_componentsUri = string.Format(componentsFormat, _apiUri);
			_statisticsUri = string.Format(statisticsFormat, _apiUri);
            _culture = culture;
        }

		/// <summary>
		/// Initializes loading of entries and filters
		/// </summary>
		/// <returns></returns>
		public virtual async Task InitializeAsync()
        {
            await LoadFieldsAsync("body");
            await LoadFieldsAsync("charger");
            await LoadFieldsAsync("cooler");
            await LoadFieldsAsync("cpu");
            await LoadFieldsAsync("hdd");
            await LoadFieldsAsync("motherboard");
            await LoadFieldsAsync("ram");
            await LoadFieldsAsync("ssd");
            await LoadFieldsAsync("videocard");

            await LoadAllEntriesAsync("body");
            await LoadAllEntriesAsync("charger");
            await LoadAllEntriesAsync("cooler");
            await LoadAllEntriesAsync("cpu");
            await LoadAllEntriesAsync("hdd");
            await LoadAllEntriesAsync("motherboard");
            await LoadAllEntriesAsync("ram");
            await LoadAllEntriesAsync("ssd");
            await LoadAllEntriesAsync("videocard");
		}

        /// <summary>
        /// Loads all records of a specified component
        /// </summary>
        /// <param name="type">Component name</param>
        /// <returns></returns>
        public async Task LoadAllEntriesAsync(string type)
        {
			string request = string.Format(GetAllElementsRequestFormat, _componentsUri, type);
			try
			{
				switch (type)
				{
					case "body":
					{
						request = request.Replace("body", "bodie");

						Bodies = (await SendHTTPRequestAsync<IEnumerable<Body>>("GET", request)).ToList();
						OnPropertyChanged("Bodies");
					}
					break;
					case "charger":
					{
						Chargers = (await SendHTTPRequestAsync<IEnumerable<Charger>>("GET", request)).ToList();
						OnPropertyChanged("Chargers");
					}
					break;
					case "cooler":
					{
						Coolers = (await SendHTTPRequestAsync<IEnumerable<Cooler>>("GET", request)).ToList();
						OnPropertyChanged("Coolers");
					}
					break;
					case "cpu":
					{
						CPUs = (await SendHTTPRequestAsync<IEnumerable<CPU>>("GET", request)).ToList();
						OnPropertyChanged("Cpus");
					}
					break;
					case "hdd":
					{
						HDDs = (await SendHTTPRequestAsync<IEnumerable<HDD>>("GET", request)).ToList();
						OnPropertyChanged("Hdds");
					}
					break;
					case "motherboard":
					{
						Motherboards = (await SendHTTPRequestAsync<IEnumerable<Motherboard>>("GET", request)).ToList();
						OnPropertyChanged("Motherboards");
					}
					break;
					case "ram":
					{
						RAMs = (await SendHTTPRequestAsync<IEnumerable<RAM>>("GET", request)).ToList();
						OnPropertyChanged("Rams");
					}
					break;
					case "ssd":
					{
						SSDs = (await SendHTTPRequestAsync<IEnumerable<SSD>>("GET", request)).ToList();
						OnPropertyChanged("Ssds");
					}
					break;
					case "videocard":
					{
						Videocards = (await SendHTTPRequestAsync<IEnumerable<Videocard>>("GET", request)).ToList();
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
        public async Task LoadFieldsAsync(string type)
        {
			string request = string.Format(GetPropertiesRequestFormat, _componentsUri, type, _culture);

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
					var v = await SendHTTPRequestAsync<Dictionary<string, Option>>("GET", request);

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
		public async Task ToggleFilterAsync(string type, string property, string value)
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
                    await UpdateDataAsync(type);
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
                    await UpdateDataAsync(type);
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
                    await UpdateDataAsync(type);
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
                    await UpdateDataAsync(type);
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
                    await UpdateDataAsync(type);
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
                    await UpdateDataAsync(type);
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
                    await UpdateDataAsync(type);
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
                    await UpdateDataAsync(type);
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
                    await UpdateDataAsync(type);
                }
                break;
            }
        }

		/// <summary>
		/// Reloads all of the records
		/// </summary>
		/// <returns></returns>
        public async Task UpdateAllDataAsync()
        {
            await UpdateDataAsync("body");
            await UpdateDataAsync("charger");
            await UpdateDataAsync("cooler");
            await UpdateDataAsync("cpu");
            await UpdateDataAsync("hdd");
            await UpdateDataAsync("motherboard");
            await UpdateDataAsync("ram");
            await UpdateDataAsync("ssd");
            await UpdateDataAsync("videocard");
        }

		/// <summary>
		/// Reloads only records of selected component
		/// </summary>
		/// <param name="type">Component</param>
		/// <returns></returns>
        public async Task UpdateDataAsync(string type)
        {
			StringBuilder request = new StringBuilder();
			StringBuilder recommendationsRequest = new StringBuilder();

            switch (type)
            {
                case "body":
                {
                    if (_selectedBodyFields.Count > 0)
                    {
						List<string> properties = _selectedBodyFields.
							Select(
								(kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))
							).ToList();
						string requestProperties = string.Join("&", properties);
						request.AppendFormat(ElementsRequestFormat, _componentsUri, type, requestProperties);
						recommendationsRequest.AppendFormat(GetRecommendationsRequestFormat, _statisticsUri, type, requestProperties, "week");
                    }

					AddSelectedToRequest(request);

					try
					{
						Bodies = (await SendHTTPRequestAsync<IEnumerable<Body>>("GET", request.ToString())).ToList();

						Dictionary<int, int> recommendations = (await SendHTTPRequestAsync<Dictionary<int, int>>("GET", recommendationsRequest.ToString()));

						foreach(var recommendation in recommendations)
						{
							var body = Bodies.First(e => e.ID == recommendation.Key);
							_bodies.Remove(body);
							_bodies.Insert(recommendation.Value, body);
						}

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
						List<string> properties = _selectedChargerFields.
							Select(
								(kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))
							).ToList();
						string requestProperties = string.Join("&", properties);
						request.AppendFormat(ElementsRequestFormat, _componentsUri, type, requestProperties);
						recommendationsRequest.AppendFormat(GetRecommendationsRequestFormat, _statisticsUri, type, requestProperties, "week");
					}

					AddSelectedToRequest(request);

					try
					{
						Chargers = (await SendHTTPRequestAsync<IEnumerable<Charger>>("GET", request.ToString())).ToList();

						Dictionary<int, int> recommendations = (await SendHTTPRequestAsync<Dictionary<int, int>>("GET", recommendationsRequest.ToString()));

						foreach (var recommendation in recommendations)
						{
							var charger = Chargers.First(e => e.ID == recommendation.Key);
							_chargers.Remove(charger);
							_chargers.Insert(recommendation.Value, charger);
						}

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
						List<string> properties = _selectedCoolerFields.
							Select(
								(kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))
							).ToList();
						string requestProperties = string.Join("&", properties);
						request.AppendFormat(ElementsRequestFormat, _componentsUri, type, requestProperties);
						recommendationsRequest.AppendFormat(GetRecommendationsRequestFormat, _statisticsUri, type, requestProperties, "week");
					}

					AddSelectedToRequest(request);

					try
					{
						Coolers = (await SendHTTPRequestAsync<IEnumerable<Cooler>>("GET", request.ToString())).ToList();

						Dictionary<int, int> recommendations = (await SendHTTPRequestAsync<Dictionary<int, int>>("GET", recommendationsRequest.ToString()));

						foreach (var recommendation in recommendations)
						{
							var cooler = Coolers.First(e => e.ID == recommendation.Key);
							_coolers.Remove(cooler);
							_coolers.Insert(recommendation.Value, cooler);
						}

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
						List<string> properties = _selectedCpuFields.
							Select(
								(kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))
							).ToList();
						string requestProperties = string.Join("&", properties);
						request.AppendFormat(ElementsRequestFormat, _componentsUri, type, requestProperties);
						recommendationsRequest.AppendFormat(GetRecommendationsRequestFormat, _statisticsUri, type, requestProperties, "week");
					}

					AddSelectedToRequest(request);

					try
					{
						CPUs = (await SendHTTPRequestAsync<IEnumerable<CPU>>("GET", request.ToString())).ToList();

						Dictionary<int, int> recommendations = (await SendHTTPRequestAsync<Dictionary<int, int>>("GET", recommendationsRequest.ToString()));

						foreach (var recommendation in recommendations)
						{
							var cpu = CPUs.First(e => e.ID == recommendation.Key);
							_cpus.Remove(cpu);
							_cpus.Insert(recommendation.Value, cpu);
						}

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
						List<string> properties = _selectedHddFields.
							Select(
								(kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))
							).ToList();
						string requestProperties = string.Join("&", properties);
						request.AppendFormat(ElementsRequestFormat, _componentsUri, type, requestProperties);
						recommendationsRequest.AppendFormat(GetRecommendationsRequestFormat, _statisticsUri, type, requestProperties, "week");
					}

					AddSelectedToRequest(request);

					try
					{
						HDDs = (await SendHTTPRequestAsync<IEnumerable<HDD>>("GET", request.ToString())).ToList();

						Dictionary<int, int> recommendations = (await SendHTTPRequestAsync<Dictionary<int, int>>("GET", recommendationsRequest.ToString()));

						foreach (var recommendation in recommendations)
						{
							var hdd = HDDs.First(e => e.ID == recommendation.Key);
							_hdds.Remove(hdd);
							_hdds.Insert(recommendation.Value, hdd);
						}

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
						List<string> properties = _selectedMotherboardFields.
							Select(
								(kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))
							).ToList();
						string requestProperties = string.Join("&", properties);
						request.AppendFormat(ElementsRequestFormat, _componentsUri, type, requestProperties);
						recommendationsRequest.AppendFormat(GetRecommendationsRequestFormat, _statisticsUri, type, requestProperties, "week");
					}

					AddSelectedToRequest(request);

					try
					{
						Motherboards = (await SendHTTPRequestAsync<IEnumerable<Motherboard>>("GET", request.ToString())).ToList();

						Dictionary<int, int> recommendations = (await SendHTTPRequestAsync<Dictionary<int, int>>("GET", recommendationsRequest.ToString()));

						foreach (var recommendation in recommendations)
						{
							var motherboard = Motherboards.First(e => e.ID == recommendation.Key);
							_motherboards.Remove(motherboard);
							_motherboards.Insert(recommendation.Value, motherboard);
						}

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
						List<string> properties = _selectedRamFields.
							Select(
								(kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))
							).ToList();
						string requestProperties = string.Join("&", properties);
						request.AppendFormat(ElementsRequestFormat, _componentsUri, type, requestProperties);
						recommendationsRequest.AppendFormat(GetRecommendationsRequestFormat, _statisticsUri, type, requestProperties, "week");
					}

					AddSelectedToRequest(request);

					try
					{
						RAMs = (await SendHTTPRequestAsync<IEnumerable<RAM>>("GET", request.ToString())).ToList();

						Dictionary<int, int> recommendations = (await SendHTTPRequestAsync<Dictionary<int, int>>("GET", recommendationsRequest.ToString()));

						foreach (var recommendation in recommendations)
						{
							var ram = RAMs.First(e => e.ID == recommendation.Key);
							_rams.Remove(ram);
							_rams.Insert(recommendation.Value, ram);
						}

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
						List<string> properties = _selectedSsdFields.
							Select(
								(kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))
							).ToList();
						string requestProperties = string.Join("&", properties);
						request.AppendFormat(ElementsRequestFormat, _componentsUri, type, requestProperties);
						recommendationsRequest.AppendFormat(GetRecommendationsRequestFormat, _statisticsUri, type, requestProperties, "week");
					}

					AddSelectedToRequest(request);

					try
					{
						SSDs = (await SendHTTPRequestAsync<IEnumerable<SSD>>("GET", request.ToString())).ToList();

						Dictionary<int, int> recommendations = (await SendHTTPRequestAsync<Dictionary<int, int>>("GET", recommendationsRequest.ToString()));

						foreach (var recommendation in recommendations)
						{
							var ssd = SSDs.First(e => e.ID == recommendation.Key);
							_ssds.Remove(ssd);
							_ssds.Insert(recommendation.Value, ssd);
						}

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
						List<string> properties = _selectedVideocardFields.
							Select(
								(kv) => string.Join("&", kv.Value.Select(v => $"{kv.Key}={v}"))
							).ToList();
						string requestProperties = string.Join("&", properties);
						request.AppendFormat(ElementsRequestFormat, _componentsUri, type, requestProperties);
						recommendationsRequest.AppendFormat(GetRecommendationsRequestFormat, _statisticsUri, type, requestProperties, "week");
					}

					AddSelectedToRequest(request);

					try
					{
						Videocards = (await SendHTTPRequestAsync<IEnumerable<Videocard>>("GET", request.ToString())).ToList();

						Dictionary<int, int> recommendations = (await SendHTTPRequestAsync<Dictionary<int, int>>("GET", recommendationsRequest.ToString()));

						foreach (var recommendation in recommendations)
						{
							var videocard = Videocards.First(e => e.ID == recommendation.Key);
							_videocards.Remove(videocard);
							_videocards.Insert(recommendation.Value, videocard);
						}

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
                    SelectedCPU = CPUs.FirstOrDefault((o) => o.ID == id);
				}
                break;
                case "hdd":
                {
                    SelectedHDD = HDDs.FirstOrDefault((o) => o.ID == id);
				}
                break;
                case "motherboard":
                {
                    SelectedMotherboard = Motherboards.FirstOrDefault((o) => o.ID == id);
				}
                break;
                case "ram":
                {
                    SelectedRAM = RAMs.FirstOrDefault((o) => o.ID == id);
				}
                break;
                case "ssd":
                {
                    SelectedSSD = SSDs.FirstOrDefault((o) => o.ID == id);
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
        public async Task ClearSelectedItemAsync(string type)
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
            await UpdateAllDataAsync();
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
                            SelectedCPU = SelectedCPU,
                            SelectedHDD = SelectedHDD,
                            SelectedMotherboard = SelectedMotherboard,
                            SelectedRAM = SelectedRAM,
                            SelectedSSD = SelectedSSD,
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
                            SelectedCPU = SelectedCPU,
                            SelectedHDD = SelectedHDD,
                            SelectedMotherboard = SelectedMotherboard,
                            SelectedRAM = SelectedRAM,
                            SelectedSSD = SelectedSSD,
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
                        SelectedCPU = info.SelectedCPU;
                        OnPropertyChanged("SelectedCpu");
                        SelectedHDD = info.SelectedHDD;
                        OnPropertyChanged("SelectedHdd");
                        SelectedMotherboard = info.SelectedMotherboard;
                        OnPropertyChanged("SelectedMotherboard");
                        SelectedRAM = info.SelectedRAM;
                        OnPropertyChanged("SelectedRam");
                        SelectedSSD = info.SelectedSSD;
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
                        SelectedCPU = info.SelectedCPU;
                        OnPropertyChanged("SelectedCpu");
                        SelectedHDD = info.SelectedHDD;
                        OnPropertyChanged("SelectedHdd");
                        SelectedMotherboard = info.SelectedMotherboard;
                        OnPropertyChanged("SelectedMotherboard");
                        SelectedRAM = info.SelectedRAM;
                        OnPropertyChanged("SelectedRam");
                        SelectedSSD = info.SelectedSSD;
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
        public async Task ChangeCultureAsync(string newCulture)
        {
            if (newCulture != null)
            {
                _culture = newCulture;
                await InitializeAsync();
            }
        }

		/// <summary>
		/// Adds component element
		/// </summary>
		/// <param name="type">Component</param>
		/// <param name="model">Element to add</param>
		/// <returns></returns>
		public async Task AddModelAsync(string type, object model, string token)
		{
			string request = string.Format(AddElementRequestFormat, _componentsUri, type);

			switch (type)
			{
				case "body":
				{
					if (model is Body m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<Body>>("POST", request, content, token);

						Bodies = res.ToList();
						OnPropertyChanged("Bodies");
					}
				}
				break;
				case "charger":
				{
					if (model is Charger m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<Charger>>("POST", request, content, token);

						Chargers = res.ToList();
						OnPropertyChanged("Chargers");
					}
				}
				break;
				case "cooler":
				{
					if (model is Cooler m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<Cooler>>("POST", request, content, token);

						Coolers = res.ToList();
						OnPropertyChanged("Coolers");
					}
				}
				break;
				case "cpu":
				{
					if (model is CPU m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<CPU>>("POST", request, content, token);

						CPUs = res.ToList();
						OnPropertyChanged("Cpus");
					}
				}
				break;
				case "hdd":
				{
					if (model is HDD m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<HDD>>("POST", request, content, token);

						HDDs = res.ToList();
						OnPropertyChanged("Hdds");
					}
				}
				break;
				case "motherboard":
				{
					if (model is Motherboard m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<Motherboard>>("POST", request, content, token);

						Motherboards = res.ToList();
						OnPropertyChanged("Motherboards");
					}
				}
				break;
				case "ram":
				{
					if (model is RAM m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<RAM>>("POST", request, content, token);

						RAMs = res.ToList();
						OnPropertyChanged("Rams");
					}
				}
				break;
				case "ssd":
				{
					if (model is SSD m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<SSD>>("POST", request, content, token);

						SSDs = res.ToList();
						OnPropertyChanged("Ssds");
					}
				}
				break;
				case "videocard":
				{
					if (model is Videocard m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<Videocard>>("POST", request, content, token);

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
		public async Task ReplaceModelAsync(string type, int id, object model, string token)
		{
			string request = string.Format(ElementByIDRequestFormat, _componentsUri, type, id);

			switch (type)
			{
				case "body":
				{
					if (model is Body m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<Body>>("PUT", request, content, token);

						Bodies = res.ToList();
						OnPropertyChanged("Bodies");
					}
				}
				break;
				case "charger":
				{
					if (model is Charger m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<Charger>>("PUT", request, content, token);

						Chargers = res.ToList();
						OnPropertyChanged("Chargers");
					}
				}
				break;
				case "cooler":
				{
					if (model is Cooler m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<Cooler>>("PUT", request, content, token);

						Coolers = res.ToList();
						OnPropertyChanged("Coolers");
					}
				}
				break;
				case "cpu":
				{
					if (model is CPU m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<CPU>>("PUT", request, content, token);

						CPUs = res.ToList();
						OnPropertyChanged("Cpus");
					}
				}
				break;
				case "hdd":
				{
					if (model is HDD m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<HDD>>("PUT", request, content, token);

						HDDs = res.ToList();
						OnPropertyChanged("Hdds");
					}
				}
				break;
				case "motherboard":
				{
					if (model is Motherboard m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<Motherboard>>("PUT", request, content, token);

						Motherboards = res.ToList();
						OnPropertyChanged("Motherboards");
					}
				}
				break;
				case "ram":
				{
					if (model is RAM m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<RAM>>("PUT", request, content, token);

						RAMs = res.ToList();
						OnPropertyChanged("Rams");
					}
				}
				break;
				case "ssd":
				{
					if (model is SSD m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<SSD>>("PUT", request, content, token);

						SSDs = res.ToList();
						OnPropertyChanged("Ssds");
					}
				}
				break;
				case "videocard":
				{
					if (model is Videocard m)
					{
						var content = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.Indented);

						var res = await SendHTTPRequestAsync<IEnumerable<Videocard>>("PUT", request, content, token);

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
		public async Task DeleteModelAsync(string type, int id, string token)
		{
			string request = string.Format(ElementByIDRequestFormat, _componentsUri, type, id);

			switch (type)
			{
				case "body":
				{
					var res = await SendHTTPRequestAsync<IEnumerable<Body>>("DELETE", request, token: token);

					Bodies = res.ToList();

					OnPropertyChanged("Bodies");
				}
				break;
				case "charger":
				{
					var res = await SendHTTPRequestAsync<IEnumerable<Charger>>("DELETE", request, token: token);

					Chargers = res.ToList();

					OnPropertyChanged("Chargers");
				}
				break;
				case "cooler":
				{
					var res = await SendHTTPRequestAsync<IEnumerable<Cooler>>("DELETE", request, token: token);

					Coolers = res.ToList();

					OnPropertyChanged("Coolers");
				}
				break;
				case "cpu":
				{
					var res = await SendHTTPRequestAsync<IEnumerable<CPU>>("DELETE", request, token: token);

					CPUs = res.ToList();

					OnPropertyChanged("Cpus");
				}
				break;
				case "hdd":
				{
					var res = await SendHTTPRequestAsync<IEnumerable<HDD>>("DELETE", request, token: token);

					HDDs = res.ToList();

					OnPropertyChanged("Hdds");
				}
				break;
				case "motherboard":
				{
					var res = await SendHTTPRequestAsync<IEnumerable<Motherboard>>("PUT", request, token: token);

					Motherboards = res.ToList();

					OnPropertyChanged("Motherboards");
				}
				break;
				case "ram":
				{
					var res = await SendHTTPRequestAsync<IEnumerable<RAM>>("DELETE", request, token: token);

					RAMs = res.ToList();

					OnPropertyChanged("Rams");
				}
				break;
				case "ssd":
				{
					var res = await SendHTTPRequestAsync<IEnumerable<SSD>>("PUT", request, token: token);

					SSDs = res.ToList();

					OnPropertyChanged("Ssds");
				}
				break;
				case "videocard":
				{
					var res = await SendHTTPRequestAsync<IEnumerable<Videocard>>("PUT", request, token: token);

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
		protected T SendHTTPRequest<T>(string method, string path, string content = null, string token = null)
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
				response = (HttpWebResponse)request.GetResponse();
			}
			catch (WebException)
			{
				throw;
			}
			catch (Exception)
			{
				throw;
			}

			if (response.StatusCode == HttpStatusCode.OK)
			{
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					string res = reader.ReadToEnd();
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
		/// Async version of <see cref="SendHTTPRequest{T}(string, string, string, string)"/>. 
		/// Sends HTTP request with optional JSON body and authorization token
		/// </summary>
		/// <typeparam name="T">Return value expected type</typeparam>
		/// <param name="method">HTTP method</param>
		/// <param name="path">Request uri</param>
		/// <param name="content">JSON content</param>
		/// <param name="token">Authorization token</param>
		/// <returns></returns>
		protected async Task<T> SendHTTPRequestAsync<T>(string method, string path, string content = null, string token = null)
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
			catch (Exception)
			{
				throw;
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
		/// Sends HTTP request with optional JSON body and authorization token
		/// </summary>
		/// <param name="method">HTTP method</param>
		/// <param name="path">Request uri</param>
		/// <param name="content">JSON content</param>
		/// <param name="token">Authorization token</param>
		/// <returns>True is success. False otherwise</returns>
		protected bool SendHTTPRequest(string method, string path, string content = null, string token = null)
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
				response = (HttpWebResponse)request.GetResponse();
			}
			catch (WebException)
			{
				throw;
			}
			catch (Exception)
			{
				throw;
			}

			if (response.StatusCode != HttpStatusCode.OK)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Async version of <see cref="SendHTTPRequest(string, string, string, string)"/>. 
		/// Sends HTTP request with optional JSON body and authorization token
		/// </summary>
		/// <param name="method">HTTP method</param>
		/// <param name="path">Request uri</param>
		/// <param name="content">JSON content</param>
		/// <param name="token">Authorization token</param>
		/// <returns>True is success. False otherwise</returns>
		protected async Task<bool> SendHTTPRequestAsync(string method, string path, string content = null, string token = null)
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
			catch (Exception)
			{
				throw;
			}

			if (response.StatusCode != HttpStatusCode.OK)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Adds selected components ID`s to the request
		/// </summary>
		/// <param name="initialRequest">Initial request</param>
		/// <returns></returns>
		protected string AddSelectedToRequest(string initialRequest)
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
			if (SelectedCPU != null)
			{
				if (request.Last() == '?')
					request = $"{request}selected-cpu={SelectedCPU.ID}";
				else
					request = $"{request}&selected-cpu={SelectedCPU.ID}";
			}
			if (SelectedHDD != null)
			{
				if (request.Last() == '?')
					request = $"{request}selected-hdd={SelectedHDD.ID}";
				else
					request = $"{request}&selected-hdd={SelectedHDD.ID}";
			}
			if (SelectedMotherboard != null)
			{
				if (request.Last() == '?')
					request = $"{request}selected-motherboard={SelectedMotherboard.ID}";
				else
					request = $"{request}&selected-motherboard={SelectedMotherboard.ID}";
			}
			if (SelectedRAM != null)
			{
				if (request.Last() == '?')
					request = $"{request}selected-ram={SelectedRAM.ID}";
				else
					request = $"{request}&selected-ram={SelectedRAM.ID}";
			}
			if (SelectedSSD != null)
			{
				if (request.Last() == '?')
					request = $"{request}selected-ssd={SelectedSSD.ID}";
				else
					request = $"{request}&selected-ssd={SelectedSSD.ID}";
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

		/// <summary>
		/// Adds selected components ID`s to the request
		/// </summary>
		/// <param name="initialRequest">Initial request</param>
		/// <returns></returns>
		protected void AddSelectedToRequest(StringBuilder initialRequest)
		{
			if (SelectedBody != null)
			{
				if (initialRequest.ToString().Last() == '?')
					initialRequest.AppendFormat("selected-body={0}", SelectedBody.ID);
				else
					initialRequest.AppendFormat("&selected-body={0}", SelectedBody.ID);
			}
			if (SelectedCharger != null)
			{
				if (initialRequest.ToString().Last() == '?')
					initialRequest.AppendFormat("selected-charger={0}", SelectedCharger.ID);
				else
					initialRequest.AppendFormat("&selected-charger={0}", SelectedCharger.ID);
			}
			if (SelectedCooler != null)
			{
				if (initialRequest.ToString().Last() == '?')
					initialRequest.AppendFormat("selected-cooler={0}", SelectedCooler.ID);
				else
					initialRequest.AppendFormat("&selected-cooler={0}", SelectedCooler.ID);
			}
			if (SelectedCPU != null)
			{
				if (initialRequest.ToString().Last() == '?')
					initialRequest.AppendFormat("selected-cpu={0}", SelectedCPU.ID);
				else
					initialRequest.AppendFormat("&selected-cpu={0}", SelectedCPU.ID);
			}
			if (SelectedHDD != null)
			{
				if (initialRequest.ToString().Last() == '?')
					initialRequest.AppendFormat("selected-hdd={0}", SelectedHDD.ID);
				else
					initialRequest.AppendFormat("&selected-hdd={0}", SelectedHDD.ID);
			}
			if (SelectedMotherboard != null)
			{
				if (initialRequest.ToString().Last() == '?')
					initialRequest.AppendFormat("selected-motherboard={0}", SelectedMotherboard.ID);
				else
					initialRequest.AppendFormat("&selected-motherboard={0}", SelectedMotherboard.ID);
			}
			if (SelectedRAM != null)
			{
				if (initialRequest.ToString().Last() == '?')
					initialRequest.AppendFormat("selected-ram={0}", SelectedRAM.ID);
				else
					initialRequest.AppendFormat("&selected-ram={0}", SelectedRAM.ID);
			}
			if (SelectedSSD != null)
			{
				if (initialRequest.ToString().Last() == '?')
					initialRequest.AppendFormat("selected-ssd={0}", SelectedSSD.ID);
				else
					initialRequest.AppendFormat("&selected-ssd={0}", SelectedSSD.ID);
			}
			if (SelectedVideocard != null)
			{
				if (initialRequest.ToString().Last() == '?')
					initialRequest.AppendFormat("selected-videocard={0}", SelectedVideocard.ID);
				else
					initialRequest.AppendFormat("&selected-videocard={0}", SelectedVideocard.ID);
			}
		}
	}
}
