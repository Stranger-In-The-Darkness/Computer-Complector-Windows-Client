using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Model.Models.Data;

namespace Model
{
	public class LoginModel : INotifyPropertyChanged
	{
		private string _uri = null;

		private User _user = new User();

		public LoginModel(string uri)
		{
			_uri = uri;
		}

		public User User { get => _user; private set => _user = value ?? _user; }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public async Task<User> Login(string username, string password)
		{
			string request = $"{_uri}/authenticate";

			HttpWebRequest post = (HttpWebRequest)WebRequest.Create(request);
			post.ContentType = "application/json";
			post.Method = "POST";

			using (var streamWriter = new StreamWriter(post.GetRequestStream()))
			{
				string json = "{\"name\":\""+username+"\"," +
							  "\"password\":\""+password+"\"}";

				streamWriter.Write(json);
			}

			HttpWebResponse response = null;
			try
			{
				response = (HttpWebResponse) await post.GetResponseAsync();
			}
			catch (WebException)
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
						User = JsonConvert.DeserializeObject<User>(res);
						OnPropertyChanged("User");
						return User;
					}
					catch (JsonSerializationException) { throw; };
				}
			}
			else
			{
				return null;
			}
		}
	}
}