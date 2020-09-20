using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Special
{
	[Serializable]
	public class RuleRelation
	{
		public int ID { get; set; }
		public string Relation { get; set; }

		public RuleRelation()
		{
			ID = -1;
			Relation = string.Empty;
		}

		public RuleRelation(int id,string relation)
		{
			ID = id;
			Relation = relation;
		}

		public override bool Equals(object obj)
		{
			if (obj is RuleRelation relation)
			{
				return Relation.Equals(relation.Relation);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
