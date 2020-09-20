using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Model.Models.Special;
using Newtonsoft.Json;

namespace Model
{
	public class AdminModel : Model
	{
		private List<Rule> _rules = new List<Rule>();
		private List<string> _realtions = new List<string>();

		private const string CompatibilityRuleRequest =
			"{0}/compatibility/rules";
		private const string GetCompatibilityRulesRequest =
			"{0}/compatibility/rules?component={1}";
		private const string GetCompatibilityRulesRelationsRequest =
			"{0}/compatibility/rules/relations";
		

		public IList<Rule> Rules { get => _rules.AsReadOnly(); }
		public IList<string> Relations { get => _realtions.AsReadOnly(); }

		public AdminModel(string apiUri, string componentsFormat, string statisticsFromat, string culture = "en") : 
			base(apiUri, componentsFormat, statisticsFromat, culture)
		{
		}

		public async override Task InitializeAsync()
		{
			await LoadCompatibilityRulesAsync("body");
			await LoadCompatibilityRulesAsync("charger");
			await LoadCompatibilityRulesAsync("cooler");
			await LoadCompatibilityRulesAsync("cpu");
			await LoadCompatibilityRulesAsync("hdd");
			await LoadCompatibilityRulesAsync("motherboard");
			await LoadCompatibilityRulesAsync("ram");
			await LoadCompatibilityRulesAsync("ssd");
			await LoadCompatibilityRulesAsync("videocard");
			await base.InitializeAsync();
		}

		public async Task ChangeProperty(string component, string name, Option newOption, string securityToken)
		{
			string uri = $"{_apiUri}/{component}/properties";
			switch (component)
			{
				case "body":
				case "charger":
				case "cooler":
				case "cpu":
				case "hdd":
				case "motherboard":
				case "ram":
				case "ssd":
				case "videocard":
				{
					string content = JsonConvert.SerializeObject(new KeyValuePair<string, Option>(name, newOption));
					Dictionary<string, Option> res =
						await SendHTTPRequestAsync<Dictionary<string, Option>>("PUT", uri, content, securityToken);
					break;
				}
			}
		}

		public async Task LoadCompatibilityRulesAsync(string type)
		{
			try
			{
				string request = string.Format(GetCompatibilityRulesRequest, _componentsUri, type);

				var res = await SendHTTPRequestAsync<List<Rule>>("GET", request);

				foreach(var rule in res)
				{
					if (!_rules.Contains(rule))
					{
						_rules.Add(rule);
					}
				}
				OnPropertyChanged("Rules");
			}
			catch (WebException)
			{
				IsFaulted = true;
				OnPropertyChanged("IsFaulted");
				return;
			}
		}

		public async Task LoadCompatibilityRulesRelationsAsync()
		{
			try
			{
				string request = string.Format(GetCompatibilityRulesRelationsRequest, _componentsUri);

				var res = await SendHTTPRequestAsync<IEnumerable<string>>("GET", request);

				_realtions = res.ToList();

				OnPropertyChanged("Realtions");
			}
			catch (WebException)
			{
				IsFaulted = true;
				OnPropertyChanged("IsFaulted");
				return;
			}
		}

		public async Task AddCompatibilityRuleAsync(Rule rule, string securityToken)
		{
			string request = string.Format(CompatibilityRuleRequest, _componentsUri);

			string content = JsonConvert.SerializeObject(rule, Formatting.Indented);

			bool res = await SendHTTPRequestAsync("POST", request, content, securityToken);
		}

		public async Task DeleteCompatibilityRuleAsync(Rule rule, string securityToken)
		{
			string request = string.Format(CompatibilityRuleRequest, _componentsUri);

			string content = JsonConvert.SerializeObject(rule, Formatting.Indented);

			bool res = await SendHTTPRequestAsync("DELETE", request, content, securityToken);
		}

		public async Task ReplaceCompatibilityRule(Rule oldRule, Rule newRule, string securityToken)
		{
			string request = string.Format(CompatibilityRuleRequest, _componentsUri);

			string content = JsonConvert.SerializeObject(new Tuple<Rule, Rule>(oldRule, newRule), Formatting.Indented);

			bool res = await SendHTTPRequestAsync("PUT", request, content, securityToken);
		}
	}
}
