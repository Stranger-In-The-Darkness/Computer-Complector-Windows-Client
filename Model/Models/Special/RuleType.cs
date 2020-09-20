using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Models.Special
{
	[Serializable]
	public class RuleType
	{
		public int ID { get; set; }
		public string Type { get; set; }

		public RuleType()
		{
			ID = -1;
			Type = string.Empty;
		}

		public RuleType(int id, string type)
		{
			ID = id;
			Type = type;
		}

		public override bool Equals(object obj)
		{
			if (obj is RuleType rule)
			{
				return Type.Equals(rule.Type);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
