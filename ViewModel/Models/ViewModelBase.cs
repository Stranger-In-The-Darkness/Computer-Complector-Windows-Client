using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Models
{
	public abstract class ViewModelBase : IDataErrorInfo, INotifyPropertyChanged, ICloneable
	{
		protected Dictionary<string, string> _errors = new Dictionary<string, string>();

		public event PropertyChangedEventHandler PropertyChanged;

		public virtual string this[string columnName] => _errors.ContainsKey(columnName) ? _errors[columnName] : null;

		public virtual string Error
		{
			get
			{
				StringBuilder error = new StringBuilder();
				if (_errors.Count == 1)
				{
					error.Append("1 error!");
				}
				else if (_errors.Count > 1)
				{
					error.AppendFormat("{0} errors!", _errors.Count);
				}
				return error.ToString();
			}
		}

		public virtual void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		/// <summary>
		/// Validate that string is neither null, nor empty, nor white space.
		/// </summary>
		/// <param name="stringToValidate">String to be validated</param>
		/// <returns>True - if string is valid. False otherwise</returns>
		protected virtual bool ValidateStringNotNullNotEmptyNotWhiteSpace(string stringToValidate)
		{
			if (string.IsNullOrEmpty(stringToValidate) || string.IsNullOrWhiteSpace(stringToValidate))
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Validate that int is not lesser then value.
		/// </summary>
		/// <param name="intToValidate">Int to be validated</param>
		/// <param name="checkValue">Value to compare to</param>
		/// <returns>True - if int is valid. False otherwise</returns>
		protected virtual bool ValidateIntNotLessThanValue(int intToValidate, int checkValue)
		{
			if (intToValidate < checkValue)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Validate that double is not lesser then value.
		/// </summary>
		/// <param name="doubleToValidate">Double to be validated</param>
		/// <param name="checkValue">Value to compare to</param>
		/// <returns>True - if int is valid. False otherwise</returns>
		protected virtual bool ValidateDoubleNotLessThanValue(double doubleToValidate, double checkValue)
		{
			if (doubleToValidate < checkValue)
			{
				return false;
			}
			return true;
		}

		public virtual object Clone()
		{
			throw new NotImplementedException();
		}
	}
}
