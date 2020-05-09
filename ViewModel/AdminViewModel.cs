using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Model;
using ViewModel.Interfaces;

namespace ViewModel
{
	public class AdminViewModel : ViewModel
	{
		public AdminViewModel(IDialogService service, string culture = "en") : base(service, culture)
		{
		}

		public AdminViewModel(IDialogService service, string serviceUri, string culture = "en") : base(service, serviceUri, culture)
		{
		}

		private string _selectedPanel = "Elements";
		public string SelectedPanel { get => _selectedPanel; set { _selectedPanel = value; OnPropertyChanged("SelectedPanel"); } }

		private User _user;
		public User User { get => _user; set { _user = value ?? _user; OnPropertyChanged("User"); } }

		private RelayCommand _addElement;
		public RelayCommand AddElement
		{
			get
			{
				return _addElement ??
					(_addElement = new RelayCommand(
						async obj =>
						{
							switch (SelectedTab)
							{
								case 0:
								{
									try
									{
										await Model.AddModel("cpu", obj, User.Token);
									}
									catch { }
								}
								break;
								case 1:
								{
									try
									{
										await Model.AddModel("motherboard", obj, User.Token);
									}
									catch { }
								}
								break;
								case 2:
								{
									try
									{
										await Model.AddModel("videocard", obj, User.Token);
									}
									catch { }
								}
								break;
								case 3:
								{
									try
									{
										await Model.AddModel("ram", obj, User.Token);
									}
									catch { }
								}
								break;
								case 4:
								{
									try
									{
										await Model.AddModel("charger", obj, User.Token);
									}
									catch { }
								}
								break;
								case 5:
								{
									try
									{
										await Model.AddModel("cooler", obj, User.Token);
									}
									catch { }
								}
								break;
								case 6:
								{
									try
									{
										await Model.AddModel("ssd", obj, User.Token);
									}
									catch { }
								}
								break;
								case 7:
								{
									try
									{
										await Model.AddModel("hdd", obj, User.Token);
									}
									catch { }
								}
								break;
								case 8:
								{
									try
									{
										await Model.AddModel("body", obj, User.Token);
									}
									catch { }
								}
								break;
							}
						},
						obj =>
						{
							return true;
						}));
			}
		}

		private RelayCommand _replaceElement;
		public RelayCommand ReplaceElement
		{
			get
			{
				return _replaceElement ??
					(_replaceElement = new RelayCommand(
						async obj => 
						{
							var values = obj as Tuple<int, object>;
							switch (SelectedTab)
							{
								case 0:
								{
									try
									{
										await Model.ReplaceModel("cpu", values.Item1, values.Item2, User.Token);
									}
									catch { }
								}
								break;
								case 1:
								{
									try
									{
										await Model.ReplaceModel("motherboard", values.Item1, values.Item2, User.Token);
									}
									catch { }
								}
								break;
								case 2:
								{
									try
									{
										await Model.ReplaceModel("videocard", values.Item1, values.Item2, User.Token);
									}
									catch { }
								}
								break;
								case 3:
								{
									try
									{
										await Model.ReplaceModel("ram", values.Item1, values.Item2, User.Token);
									}
									catch { }
								}
								break;
								case 4:
								{
									try
									{
										await Model.ReplaceModel("charger", values.Item1, values.Item2, User.Token);
									}
									catch { }
								}
								break;
								case 5:
								{
									try
									{
										await Model.ReplaceModel("cooler", values.Item1, values.Item2, User.Token);
									}
									catch { }
								}
								break;
								case 6:
								{
									try
									{
										await Model.ReplaceModel("ssd", values.Item1, values.Item2, User.Token);
									}
									catch { }
								}
								break;
								case 7:
								{
									try
									{
										await Model.ReplaceModel("hdd", values.Item1, values.Item2, User.Token);
									}
									catch { }
								}
								break;
								case 8:
								{
									try
									{
										await Model.ReplaceModel("body", values.Item1, values.Item2, User.Token);
									}
									catch { }
								}
								break;
							}
						},
						obj => true));
			}
		}

		private RelayCommand _deleteElement;
		public RelayCommand DeleteElement
		{
			get
			{
				return _deleteElement ??
					(_deleteElement = new RelayCommand(
						async obj => 
						{
							switch (SelectedTab)
							{
								case 0:
								{
									try
									{
										await Model.DeleteModel("cpu", (int)obj, User.Token);
									}
									catch { }
								}
								break;
								case 1:
								{
									try
									{
										await Model.DeleteModel("motherboard", (int)obj, User.Token);
									}
									catch { }
								}
								break;
								case 2:
								{
									try
									{
										await Model.DeleteModel("videocard", (int)obj, User.Token);
									}
									catch { }
								}
								break;
								case 3:
								{
									try
									{
										await Model.DeleteModel("ram", (int)obj, User.Token);
									}
									catch { }
								}
								break;
								case 4:
								{
									try
									{
										await Model.DeleteModel("charger", (int)obj, User.Token);
									}
									catch { }
								}
								break;
								case 5:
								{
									try
									{
										await Model.DeleteModel("cooler", (int)obj, User.Token);
									}
									catch { }
								}
								break;
								case 6:
								{
									try
									{
										await Model.DeleteModel("ssd", (int)obj, User.Token);
									}
									catch { }
								}
								break;
								case 7:
								{
									try
									{
										await Model.DeleteModel("hdd", (int)obj, User.Token);
									}
									catch { }
								}
								break;
								case 8:
								{
									try
									{
										await Model.DeleteModel("body", (int)obj, User.Token);
									}
									catch { }
								}
								break;
							}
						},
						obj => true));
			}
		}

		private RelayCommand _changePanel;
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
	}
}