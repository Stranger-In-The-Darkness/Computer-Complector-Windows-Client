using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace Computer_Complector
{
    public class SelectedItemConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int current = values[0] == DependencyProperty.UnsetValue ? -1 : (int)values[0];
            int selected = (int)values[1];

            if (current == selected)
            {
                return new DropShadowEffect()
                {
                    ShadowDepth = 0,
                    Color = new System.Windows.Media.Color()
                    {
                        A = 255,
                        R = 70,
                        G = 255,
                        B = 0
                    },
                    BlurRadius = 40
                };
            }
            else
            {
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
