using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

using Model;
using Newtonsoft.Json;

namespace ViewModel
{
	public class LoginViewModel : INotifyPropertyChanged
	{
		private LoginModel _model;

		public LoginViewModel(string authUri)
		{
			_model = new LoginModel(authUri);
		}

		public User User { get; private set; }

		public bool IsAuthorized { get; private set; }

		public string Error { get; set; } = string.Empty;

		public string Username { get; set; }

		public string Password { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		private RelayCommand _autheorize;
		public RelayCommand Authorize
		{
			get
			{
				return _autheorize ??
					(_autheorize = new RelayCommand(
						async (obj) => 
						{
							try
							{
								User = await _model.Login(Username, Password);
								IsAuthorized = true;
								Error = string.Empty;
								OnPropertyChanged("Error");
								OnPropertyChanged("User");
								OnPropertyChanged("IsAuthorized");
							}
							catch (WebException e)
							{
								using (var stream = e.Response.GetResponseStream())
								{
									using (var reader = new StreamReader(stream))
									{
										var error = await reader.ReadToEndAsync();
										var er = new { message = "" };
										er = JsonConvert.DeserializeAnonymousType(error, er);
										Error = er.message;
									}
								}
								OnPropertyChanged("Error");
								IsAuthorized = false;
								OnPropertyChanged("IsAuthorized");
							}
						},
						(obj) => 
						{
							if (CorrectString(Username) && CorrectString(Password))
							{
								return true;
							}
							return false;
						}
					));
			}
		}

		private bool CorrectString(string str)
		{
			return !string.IsNullOrEmpty(str) & !string.IsNullOrWhiteSpace(str);
		}
	}
}