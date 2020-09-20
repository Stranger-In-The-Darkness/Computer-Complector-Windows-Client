using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Special
{
	[Serializable]
	public class Rule
	{
		public int ID { get; set; }
		public string FirstComponent { get; set; }
		public string FirstProperty { get; set; }
		public string SecondComponent { get; set; }
		public string SecondProperty { get; set; }
		public RuleRelation Relation { get; set; }
		public RuleType RuleType { get; set; }

		public Rule()
		{
			ID = -1;
			FirstComponent = string.Empty;
			FirstProperty = string.Empty;
			SecondComponent = string.Empty;
			SecondProperty = string.Empty;
			Relation = new RuleRelation();
			RuleType = new RuleType();
		}

		public Rule(int id, string firstComponent, string firstProperty, string secondComponent,  string secondProperty, RuleRelation relation, RuleType ruleType)
		{
			ID = id;
			FirstComponent = firstComponent;
			FirstProperty = firstProperty;
			SecondComponent = secondComponent;
			SecondProperty = secondProperty;
			Relation = relation;
			RuleType = ruleType;
		}

		public override bool Equals(object obj)
		{
			if (obj is Rule rule)
			{
				return 
					FirstComponent.Equals(rule.FirstComponent) &&
					FirstProperty.Equals(rule.FirstProperty) &&
					Relation.Equals(rule.Relation) &&
					SecondComponent.Equals(rule.SecondComponent) &&
					SecondProperty.Equals(rule.SecondProperty) &&
					RuleType.Equals(rule.RuleType);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
