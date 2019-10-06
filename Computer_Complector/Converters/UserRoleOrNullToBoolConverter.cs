using System;
using System.Globalization;
using System.Windows.Data;

namespace Computer_Complector
{
	public class UserRoleOrNullToBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var user = value as Model.User;
			if (user != null && user.Role.ToUpper() == "ADMIN")
			{
				return true;
			}
			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}