using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Models.Data;
using ViewModel.Interfaces;
using ViewModel.Events.Arguments;
using ViewModel.Events.Delegates;

namespace ViewModel
{
	public class AdminViewModel : INotifyPropertyChanged
	{
		public readonly string[] TYPE = new string[]
		{
			"body",
			"charger",
			"cooler",
			"cpu",
			"hdd",
			"motherboard",
			"ram",
			"ssd",
			"videocard"
		};

		#region Fields

		private IDialogService _dialogService;

		private string _culture = string.Empty;
		private int _selectedTab = 0;
		private string _selectedElement = "body";
		private string _selectedFirstComponent = "body";
		private string _selectedSecondComponent = "charger";
		private string _selectedPanel = "Properties";

		private User _user;
		private AdminModel _model;
		private List<string> _compatibilityRules;
		private List<Rule> _rules;

		private Body _selectedBody = null;
		private Charger _selectedCharger = null;
		private Cooler _selectedCooler = null;
		private CPU _selectedCpu = null;
		private HDD _selectedHdd = null;
		private Motherboard _selectedMotherboard = null;
		private RAM _selectedRam = null;
		private SSD _selectedSsd = null;
		private Videocard _selectedVideocard = null;

		private Dictionary<string, Option> _bodyFields = new Dictionary<string, Option>();
		private Dictionary<string, Option> _chargerFields = new Dictionary<string, Option>();
		private Dictionary<string, Option> _coolerFields = new Dictionary<string, Option>();
		private Dictionary<string, Option> _cpuFields = new Dictionary<string, Option>();
		private Dictionary<string, Option> _hddFields = new Dictionary<string, Option>();
		private Dictionary<string, Option> _motherboardFields = new Dictionary<string, Option>();
		private Dictionary<string, Option> _ramFields = new Dictionary<string, Option>();
		private Dictionary<string, Option> _ssdFields = new Dictionary<string, Option>();
		private Dictionary<string, Option> _videocardFields = new Dictionary<string, Option>();

		private RelayCommand _changeTab;
		private RelayCommand _changeElement;
		private RelayCommand _changePanel;
		private RelayCommand _changeCulture;
		private RelayCommand _selectFilter;
		private RelayCommand _openFile;
		private RelayCommand _saveFile;
		private RelayCommand _selectItem;
		private RelayCommand _deselectItem;
		private RelayCommand _addElement;
		private RelayCommand _replaceElement;
		private RelayCommand _deleteElement;
		private RelayCommand _replaceProperty;
		private RelayCommand _beginAddingElement;
		private RelayCommand _beginEditingElement;
		private RelayCommand _beginEditingProperty;
		private RelayCommand _addCompatibilityRule;
		private RelayCommand _removeCompatibilityRule;
		private RelayCommand _updateCompatibilityRule;
		private RelayCommand _selectFirstComponent;
		private RelayCommand _selectSecondComponent;

		private BeginAdding _beginAdding = (_) => { };
		private BeginEditing _beginEditing = (_) => { };

		#endregion

		#region Properties

		public string Culture { get => _culture; }

		internal IDialogService DialogService { get => _dialogService; }

		public int SelectedTab { get => _selectedTab; set { _selectedTab = value; OnPropertyChanged("SelectedTab"); } }
		public string SelectedElement { get => _selectedElement; set { _selectedElement = value; OnPropertyChanged("SelectedElement"); } }
		public string SelectedFirstComponent
		{
			get => _selectedFirstComponent;
			set
			{
				_selectedFirstComponent = value;
				OnPropertyChanged("SelectedFirstComponent");
				MarkOnlyVisibleRules(SelectedFirstComponent, SelectedSecondComponent);
			}
		}
		public string SelectedSecondComponent
		{
			get => _selectedSecondComponent;
			set
			{
				_selectedSecondComponent = value;
				OnPropertyChanged("SelectedSecondComponent");
				MarkOnlyVisibleRules(SelectedFirstComponent, SelectedSecondComponent);
			}
		}
		public string SelectedPanel { get => _selectedPanel; set { _selectedPanel = value; OnPropertyChanged("SelectedPanel"); } }
		public bool OfflineMode { get; protected set; } = false;
		public IList<Rule> Rules { get => _rules?.AsReadOnly() ?? (_rules = new List<Rule>()).AsReadOnly(); private set {
				_rules = value.ToList(); OnPropertyChanged("Rules"); } }
		public IList<string> CompatibilityRules { get => _compatibilityRules?.AsReadOnly() ?? (_compatibilityRules = new List<string>()).AsReadOnly(); set { _compatibilityRules = value.ToList(); OnPropertyChanged("CompatibilityRules"); } }
		public User User { get => _user; set { _user = value ?? _user; OnPropertyChanged("User"); } }
		internal AdminModel Model {	get => _model; set { if (value != null)	{ _model = value; } } }

		public RelayCommand ChangeTab
		{
			get
			{
				return _changeTab ??
					(_changeTab = new RelayCommand(
						async obj =>
						{
							_model.Clear(TYPE[SelectedTab]);

							int selected = int.Parse(obj.ToString());
							SelectedTab = selected;

							await _model.UpdateDataAsync(TYPE[SelectedTab]);
						},
						obj =>
						{
							return int.Parse(obj.ToString()) >= 0 && int.Parse(obj.ToString()) < 9;
						}));
			}
		}
		public RelayCommand ChangeElement
		{
			get
			{
				return _changeElement ??
					(_changeElement = new RelayCommand(
						async obj =>
						{
							string element = obj.ToString();
							SelectedElement = element;

							Rules = _model.Rules.Where(e => e.FirstComponent == SelectedFirstComponent && e.SecondComponent == SelectedSecondComponent).Cast<Rule>().ToList();

							SelectedTab = TYPE.ToList().IndexOf(SelectedElement);

							_model.Clear(TYPE[SelectedTab]);
							await _model.UpdateDataAsync(TYPE[SelectedTab]);
						},
						obj =>
						{
							return obj != null;
						}));
			}
		}
		public RelayCommand ChangePanel
		{
			get
			{
				return _changePanel ??
					(_changePanel = new RelayCommand(
						obj =>
						{
							SelectedPanel = obj.ToString();
						},
						obj => true));
			}
		}
		public RelayCommand ChangeCulture
		{
			get
			{
				return _changeCulture ??
					(_changeCulture = new RelayCommand(
						(obj) =>
						{
							if (obj as string != null)
							{
								ChangeModelCulture(obj as string);
							}
						},
						_ => { return true; }));
			}
		}
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
										await _model.ToggleFilterAsync(
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
										await _model.ToggleFilterAsync(
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
										await _model.ToggleFilterAsync(
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
										await _model.ToggleFilterAsync(
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
										await _model.ToggleFilterAsync(
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
										await _model.ToggleFilterAsync(
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
										await _model.ToggleFilterAsync(
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
										await _model.ToggleFilterAsync(
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
										await _model.ToggleFilterAsync(
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
		public RelayCommand SelectItem
		{
			get
			{
				return _selectItem ??
					(_selectItem = new RelayCommand(
						(obj) =>
						{
							_model.SelectItem(TYPE[_selectedTab], (int)obj);
						},
						(obj) =>
						{
							return true;
						}
						));
			}
		}
		public RelayCommand DeselectItem
		{
			get
			{
				return _deselectItem ??
					(_deselectItem = new RelayCommand(
						async (obj) =>
						{
							await _model.ClearSelectedItemAsync(TYPE[int.Parse(obj.ToString())]);
						},
						(obj) =>
						{
							return int.TryParse(obj?.ToString() ?? "", out _);
						}
						));
			}
		}
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
		public RelayCommand AddElement
		{
			get
			{
				return _addElement ??
					(_addElement = new RelayCommand(
						async obj =>
						{
							if (obj is Tuple<string, object> modelAndData)
							{
								try
								{
									await Model.AddModelAsync(modelAndData.Item1, modelAndData.Item2, User.Token);
								}
								catch { }
							}
						},
						obj =>
						{
							if (obj != null)
							{
								return true;
							}
							return false;
						}));
			}
		}
		public RelayCommand ReplaceElement
		{
			get
			{
				return _replaceElement ??
					(_replaceElement = new RelayCommand(
						async obj =>
						{
							Tuple<string, int, object> modelAndData = (Tuple<string, int, object>)obj;
							try
							{
								await Model.ReplaceModelAsync(modelAndData.Item1, modelAndData.Item2, modelAndData.Item3, User.Token);
							}
							catch { }
						},
						_ => true));
			}
		}
		public RelayCommand DeleteElement
		{
			get
			{
				return _deleteElement ??
					(_deleteElement = new RelayCommand(
						async obj =>
						{
							int id = int.Parse(obj.ToString());
							switch (TYPE[SelectedTab])
							{
								case "cpu":
								{
									try
									{
										await Model.DeleteModelAsync("cpu", id, User.Token);
									}
									catch { }
								}
								break;
								case "motherboard":
								{
									try
									{
										await Model.DeleteModelAsync("motherboard", id, User.Token);
									}
									catch { }
								}
								break;
								case "videocard":
								{
									try
									{
										await Model.DeleteModelAsync("videocard", id, User.Token);
									}
									catch { }
								}
								break;
								case "ram":
								{
									try
									{
										await Model.DeleteModelAsync("ram", id, User.Token);
									}
									catch { }
								}
								break;
								case "charger":
								{
									try
									{
										await Model.DeleteModelAsync("charger", id, User.Token);
									}
									catch { }
								}
								break;
								case "cooler":
								{
									try
									{
										await Model.DeleteModelAsync("cooler", id, User.Token);
									}
									catch { }
								}
								break;
								case "ssd":
								{
									try
									{
										await Model.DeleteModelAsync("ssd", id, User.Token);
									}
									catch { }
								}
								break;
								case "hdd":
								{
									try
									{
										await Model.DeleteModelAsync("hdd", id, User.Token);
									}
									catch { }
								}
								break;
								case "body":
								{
									try
									{
										await Model.DeleteModelAsync("body", id, User.Token);
									}
									catch { }
								}
								break;
							}
						},
						obj => false));
			}
		}
		public RelayCommand BeginAddingElement
		{
			get
			{
				return _beginAddingElement ??
					(_beginAddingElement = new RelayCommand(
						obj =>
						{
							BeginAddingModelArgs args = new BeginAddingModelArgs()
							{
								ModelType = obj.ToString()
							};
							_beginAdding.Invoke(args);
						},
						obj => true));
			}
		}
		public RelayCommand BeginEditingElement
		{
			get
			{
				return _beginEditingElement ??
					(_beginEditingElement = new RelayCommand(
						obj =>
						{
							int id = int.Parse(obj.ToString());
							BeginEditingModelArgs args = new BeginEditingModelArgs()
							{
								ModelType = TYPE[_selectedTab]
							};
							switch (_selectedTab)
							{
								case 0:
								{
									args.Model = Bodies.FirstOrDefault(e => e.ID == id);
									break;
								}
								case 1:
								{
									args.Model = Chargers.FirstOrDefault(e => e.ID == id);
									break;
								}
								case 2:
								{
									args.Model = Coolers.FirstOrDefault(e => e.ID == id);
									break;
								}
								case 3:
								{
									args.Model = CPUs.FirstOrDefault(e => e.ID == id);
									break;
								}
								case 4:
								{
									args.Model = HDDs.FirstOrDefault(e => e.ID == id);
									break;
								}
								case 5:
								{
									args.Model = Motherboards.FirstOrDefault(e => e.ID == id);
									break;
								}
								case 6:
								{
									args.Model = RAMs.FirstOrDefault(e => e.ID == id);
									break;
								}
								case 7:
								{
									args.Model = SSDs.FirstOrDefault(e => e.ID == id);
									break;
								}
								case 8:
								{
									args.Model = Videocards.FirstOrDefault(e => e.ID == id);
									break;
								}
							}
							_beginEditing.Invoke(args);
						},
						obj =>
						{
							if (obj == null)
							{
								return false;
							}
							return true;
						}));
			}
		}
		public RelayCommand BeginEditingProperty
		{
			get
			{
				return _beginEditingProperty ?? (_beginEditingProperty = new RelayCommand(
					obj => 
					{
						if (obj is Option option)
						{
							BeginEditingModelArgs args = new BeginEditingModelArgs()
							{
								ModelType = "option",
								Model = option
							};
							_beginEditing.Invoke(args);
						}
					},
					obj =>
					{
						if (obj == null)
						{
							return false;
						}
						return true;
					}));
			}
		}
		public RelayCommand ReplaceProperty
		{
			get
			{
				return _replaceProperty ?? (_replaceProperty = new RelayCommand(
					async obj => 
					{
						if (obj is Option option)
						{
							await _model.ChangeProperty(_selectedElement, string.Join("-", option.Text.Split(' ')), option, User.Token);
						}
					},
					obj => 
					{
						if (obj != null && obj is Tuple<string, Option>)
						{
							return true;
						}
						return false;
					}));
			}
		}
		public RelayCommand AddCompatibilityRule
		{
			get
			{
				return _addCompatibilityRule ??
					(_addCompatibilityRule = new RelayCommand(
						async obj => 
						{
							if (obj is Rule rule)
							{
								await _model.AddCompatibilityRuleAsync(rule, _user.Token);
							}
						},
						obj =>
						{
							if (obj is Rule rule && rule != null)
							{
								return true;
							}
							return false;
						}));
			}
		}
		public RelayCommand RemoveCompatibilityRule
		{
			get
			{
				return _removeCompatibilityRule ??
					(_removeCompatibilityRule = new RelayCommand(
						async obj => 
						{
							if (obj is Rule rule)
							{
								await _model.DeleteCompatibilityRuleAsync(rule, _user.Token);
							}
						},
						obj =>
						{
							if (obj is Rule rule && rule != null)
							{
								return true;
							}
							return false;
						}));
			}
		}
		public RelayCommand UpdateCompatibilityRule
		{
			get
			{
				return _updateCompatibilityRule ??
					(_updateCompatibilityRule = new RelayCommand(
						async obj => 
						{
							if (obj is Tuple<Rule, Rule> rules)
							{
								await _model.ReplaceCompatibilityRule(rules.Item1, rules.Item2, _user.Token);
							}
						},
						obj =>
						{
							if (obj is Tuple<Rule, Rule> rules && rules!= null && rules.Item1 != null && rules.Item2 != null)
							{
								return true;
							}
							return false;
						}));
			}
		}
		public RelayCommand SelectFirstComponent
		{
			get
			{
				return _selectFirstComponent ?? (_selectFirstComponent = new RelayCommand(
					obj =>
					{
						if (obj is string component)
						{
							SelectedFirstComponent = component;
						}
					},
					obj =>
					{
						if (obj is string component && TYPE.Contains(component))
						{
							return true;
						}
						return false;
					}));
			}
		}
		public RelayCommand SelectSecondComponent
		{
			get
			{
				return _selectSecondComponent ?? (_selectSecondComponent = new RelayCommand(
					obj =>
					{
						if (obj is string component)
						{
							SelectedSecondComponent = component;
						}
					},
					obj =>
					{
						if (obj is string component && TYPE.Contains(component))
						{
							return true;
						}
						return false;
					}));
			}
		}

		public ObservableCollection<Body> Bodies { get; set; } = new ObservableCollection<Body>();
		public ObservableCollection<Charger> Chargers { get; set; } = new ObservableCollection<Charger>();
		public ObservableCollection<Cooler> Coolers { get; set; } = new ObservableCollection<Cooler>();
		public ObservableCollection<CPU> CPUs { get; set; } = new ObservableCollection<CPU>();
		public ObservableCollection<HDD> HDDs { get; set; } = new ObservableCollection<HDD>();
		public ObservableCollection<Motherboard> Motherboards { get; set; } = new ObservableCollection<Motherboard>();
		public ObservableCollection<RAM> RAMs { get; set; } = new ObservableCollection<RAM>();
		public ObservableCollection<SSD> SSDs { get; set; } = new ObservableCollection<SSD>();
		public ObservableCollection<Videocard> Videocards { get; set; } = new ObservableCollection<Videocard>();

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
				return new Selection()
				{
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

		public Dictionary<string, Option> BODYFields { get => _bodyFields; set { if (value != null) { _bodyFields = value; OnPropertyChanged("BODYFields"); } } }
		public Dictionary<string, Option> CHARGERFields { get => _chargerFields; set { if (value != null) { _chargerFields = value; OnPropertyChanged("CHARGERFields"); } } }
		public Dictionary<string, Option> COOLERFields { get => _coolerFields; set { if (value != null) { _coolerFields = value; OnPropertyChanged("COOLERFields"); } } }
		public Dictionary<string, Option> CPUFields { get => _cpuFields; set { if (value != null) { _cpuFields = value; OnPropertyChanged("CPUFields"); } } }
		public Dictionary<string, Option> HDDFields { get => _hddFields; set { if (value != null) { _hddFields = value; OnPropertyChanged("HDDFields"); } } }
		public Dictionary<string, Option> MOTHERBOARDFields { get => _motherboardFields; set { if (value != null) { _motherboardFields = value; OnPropertyChanged("MOTHERBOARDFields"); } } }
		public Dictionary<string, Option> RAMFields { get => _ramFields; set { if (value != null) { _ramFields = value; OnPropertyChanged("RAMFields"); } } }
		public Dictionary<string, Option> SSDFields { get => _ssdFields; set { if (value != null) { _ssdFields = value; OnPropertyChanged("SSDFields"); } } }
		public Dictionary<string, Option> VIDEOCARDFields { get => _videocardFields; set { if (value != null) { _videocardFields = value; OnPropertyChanged("VIDEOCARDFields"); } } }

		#endregion

		#region Events 

		public event BeginAdding OnBeginAddingEvent
		{
			add
			{
				_beginAdding += value;
			}
			remove
			{
				_beginAdding -= value;
			}
		}

		public event BeginEditing OnBeginEditingEvent
		{
			add
			{
				_beginEditing += value;
			}
			remove
			{
				_beginEditing -= value;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Constructors

		public AdminViewModel(IDialogService service, string serviceUri, string componentsFormat, string statisticsFormat, string culture = "en")
		{
			_culture = culture;
			_dialogService = service;
			_model = new AdminModel(serviceUri, componentsFormat, statisticsFormat, culture);
			_model.PropertyChanged += OnModelPropertyChanged;
			_model.InitializeAsync();
		}

		#endregion

		#region Methods

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
							await _model.LoadAllEntriesAsync("body");
						}
						break;
						case 1:
						{
							await _model.LoadAllEntriesAsync("charger");
						}
						break;
						case 2:
						{
							await _model.LoadAllEntriesAsync("cooler");
						}
						break;
						case 3:
						{
							await _model.LoadAllEntriesAsync("cpu");
						}
						break;
						case 4:
						{
							await _model.LoadAllEntriesAsync("hdd");
						}
						break;
						case 5:
						{
							await _model.LoadAllEntriesAsync("motherboard");
						}
						break;
						case 6:
						{
							await _model.LoadAllEntriesAsync("ram");
						}
						break;
						case 7:
						{
							await _model.LoadAllEntriesAsync("ssd");
						}
						break;
						case 8:
						{
							await _model.LoadAllEntriesAsync("videocard");
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
							await _model.LoadFieldsAsync("cpu");
						}
						break;
						case 1:
						{
							await _model.LoadFieldsAsync("motherboard");
						}
						break;
						case 2:
						{
							await _model.LoadFieldsAsync("videocard");
						}
						break;
						case 3:
						{
							await _model.LoadFieldsAsync("ram");
						}
						break;
						case 4:
						{
							await _model.LoadFieldsAsync("charger");
						}
						break;
						case 5:
						{
							await _model.LoadFieldsAsync("cooler");
						}
						break;
						case 6:
						{
							await _model.LoadFieldsAsync("ssd");
						}
						break;
						case 7:
						{
							await _model.LoadFieldsAsync("hdd");
						}
						break;
						case 8:
						{
							await _model.LoadFieldsAsync("body");
						}
						break;
					}
				}
				);
		}

		private async void ChangeModelCulture(string newCulture)
		{
			if (newCulture != null)
			{
				Task t = Task.Factory.StartNew(() => _model.ChangeCultureAsync(newCulture));
				await t;
			}
		}

		private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "Bodies":
				{
					Bodies.Clear();
					foreach (Body b in _model.Bodies)
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
					Bodies.OrderBy(b => b.CompatibilityLevel).ThenBy(b => b.ID);
					OnPropertyChanged("Bodies");
				}
				break;
				case "Chargers":
				{
					Chargers.Clear();
					foreach (Charger b in _model.Chargers)
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
					Chargers.OrderBy(b => b.CompatibilityLevel).ThenBy(b => b.ID);
					OnPropertyChanged("Chargers");
				}
				break;
				case "Coolers":
				{
					Coolers.Clear();
					foreach (Cooler b in _model.Coolers)
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
					Coolers.OrderBy(b => b.CompatibilityLevel).ThenBy(b => b.ID);
					OnPropertyChanged("Coolers");
				}
				break;
				case "Cpus":
				{
					CPUs.Clear();
					foreach (CPU b in _model.CPUs)
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
					CPUs.OrderBy(b => b.CompatibilityLevel).ThenBy(b => b.ID);
					OnPropertyChanged("CPUs");
				}
				break;
				case "Hdds":
				{
					HDDs.Clear();
					foreach (HDD b in _model.HDDs)
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
					HDDs.OrderBy(b => b.CompatibilityLevel).ThenBy(b => b.ID);
					OnPropertyChanged("HDDs");
				}
				break;
				case "Motherboards":
				{
					Motherboards.Clear();
					foreach (Motherboard b in _model.Motherboards)
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
					Motherboards.OrderBy(b => b.CompatibilityLevel).ThenBy(b => b.ID);
					OnPropertyChanged("Motherboards");
				}
				break;
				case "Rams":
				{
					RAMs.Clear();
					foreach (RAM b in _model.RAMs)
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
					RAMs.OrderBy(b => b.CompatibilityLevel).ThenBy(b => b.ID);
					OnPropertyChanged("RAMs");
				}
				break;
				case "Ssds":
				{
					SSDs.Clear();
					foreach (SSD b in _model.SSDs)
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
					SSDs.OrderBy(b => b.CompatibilityLevel).ThenBy(b => b.ID);
					OnPropertyChanged("SSDs");
				}
				break;
				case "Videocards":
				{
					Videocards.Clear();
					foreach (Videocard b in _model.Videocards)
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
					Videocards.OrderBy(b => b.CompatibilityLevel).ThenBy(b => b.ID);
					OnPropertyChanged("Videocards");
				}
				break;
				case "BodyFields":
					BODYFields = _model.BodyFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
					break;
				case "ChargerFields":
					CHARGERFields = _model.ChargerFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
					break;
				case "CoolerFields":
					COOLERFields = _model.CoolerFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
					break;
				case "CPUFields":
					CPUFields = _model.CPUFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
					break;
				case "HDDFields":
					HDDFields = _model.HDDFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
					break;
				case "MotherboardFields":
					MOTHERBOARDFields = _model.MotherboardFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
					break;
				case "RAMFields":
					RAMFields = _model.RAMFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
					break;
				case "SSDFields":
					SSDFields = _model.SSDFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
					break;
				case "VideocardFields":
					VIDEOCARDFields = _model.VideocardFields.Select((o) => new Tuple<string, Option>(o.Key, (Option)o.Value)).ToDictionary((e1) => e1.Item1, (e2) => e2.Item2);
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
					if (_model.SelectedCPU == null)
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
						SelectedCpu = _model.SelectedCPU;
					}
					OnPropertyChanged("CPUs");
					OnPropertyChanged("Selected");
					OnPropertyChanged("SelectedCpu");
					OnPropertyChanged("SelectedCpuAsEnum");
				}
				break;
				case "SelectedHdd":
				{
					if (_model.SelectedHDD == null)
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
						SelectedHdd = _model.SelectedHDD;
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
					if (_model.SelectedRAM == null)
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
						SelectedRam = _model.SelectedRAM;
					}
					OnPropertyChanged("RAMs");
					OnPropertyChanged("Selected");
					OnPropertyChanged("SelectedRam");
					OnPropertyChanged("SelectedRamAsEnum");
				}
				break;
				case "SelectedSsd":
				{
					if (_model.SelectedSSD == null)
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
						SelectedSsd = _model.SelectedSSD;
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
				case "Rules":
				{
					Rules = _model.Rules.ToList().Select(el => (Rule)el).ToList();
					MarkOnlyVisibleRules(SelectedFirstComponent, SelectedSecondComponent);
					break;
				}
				default:
					break;
			}
		}

		private void MarkOnlyVisibleRules(string firstComponent, string secondComponent)
		{
			_rules.ForEach(e => e.Show = false);

			foreach (Rule rule in _rules.Where(e => e.FirstComponent == firstComponent && e.SecondComponent == secondComponent))
			{
				rule.Show = true;
			}

			OnPropertyChanged("Rules");
		}

		#endregion
	}
}