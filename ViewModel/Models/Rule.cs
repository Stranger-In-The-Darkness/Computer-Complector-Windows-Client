using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;

using M = Model.Models.Special;

namespace ViewModel
{
	public class Rule : ViewModelBase
	{
		public string FirstComponent { get; set; }
		public string FirstProperty { get; set; }
		public string SecondComponent { get; set; }
		public string SecondProperty { get; set; }
		public string Relation { get; set; }
		public string Type { get; set; }
		public bool Show { get; set; }

		public override object Clone()
		{
			return new Rule()
			{
				FirstComponent = FirstComponent,
				FirstProperty = FirstProperty,
				SecondComponent = SecondComponent,
				SecondProperty = SecondProperty,
				Relation = Relation,
				Type = Type, Show = Show
			};
		}

		public static implicit operator Rule(M.Rule rule)
		{
			return new Rule()
			{
				FirstComponent = rule.FirstComponent,
				FirstProperty = rule.FirstProperty,
				SecondComponent = rule.SecondComponent,
				SecondProperty = rule.SecondProperty,
				Relation = rule.Relation.Relation,
				Type = rule.RuleType.Type,
				Show = true
			};
		}

		public static implicit operator M.Rule(Rule rule)
		{
			return new M.Rule()
			{
				FirstComponent = rule.FirstComponent,
				FirstProperty = rule.FirstProperty,
				SecondComponent = rule.SecondComponent,
				SecondProperty = rule.SecondProperty,
				Relation = new M.RuleRelation() { Relation = rule.Relation},
				RuleType = new M.RuleType() { Type = rule.Type}
			};
		}
	}
}
