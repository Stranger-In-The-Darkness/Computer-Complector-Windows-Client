using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;
using M = Model.Models.Special;

namespace ViewModel
{
    public class Option : ViewModelBase
    {
		private string _text;
		private IEnumerable<string> _values;

        public string Text
		{
			get => _text;
			set
			{
				_text = value;

				_errors.Remove("Text");
				if (!ValidateStringNotNullNotEmptyNotWhiteSpace(_text))
				{
					_errors.Add("Text", "Invalid data!");
				}
				OnPropertyChanged("Text");
				OnPropertyChanged("Error");
			}
		}
        public bool Addition { get; set; }
        public string AdditionText { get; set; }
        public IEnumerable<string> Values
		{
			get => _values;
			set
			{
				_values = value;

				_errors.Remove("Values");
				foreach(string val in _values)
				{
					if (!ValidateStringNotNullNotEmptyNotWhiteSpace(val))
					{
						_errors.Add("Values", "Invalid data!");
						break;
					}
				}
				OnPropertyChanged("Values");
				OnPropertyChanged("Error");
			}
		}

		public override object Clone()
		{
			return new Option()
			{
				Addition = Addition,
				AdditionText = AdditionText,
				Text = Text,
				Values = Values.ToList()
			};
		}

		public override bool Equals(object obj)
		{
			if (obj is Option option)
			{
				return Addition.Equals(option.Addition) &&
					Text.Equals(option.Text) &&
					AdditionText.Equals(option.AdditionText) &&
					Values.SequenceEqual(option.Values);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static implicit operator Option(M.Option o)
        {
            return new Option()
            {
                Text = o.Text,
                Addition = o.Addition,
                AdditionText = o.AdditionText,
                Values = o.Values
            };
        }

		public static implicit operator M.Option(Option o)
		{
			return new M.Option()
			{
				Text = o.Text,
				Addition = o.Addition,
				AdditionText = o.AdditionText,
				Values = o.Values
			};
		}
	}
}
