using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FIRSTRPGUI.CustomConverters
{
    public class FileToBitmapConverterRace : IValueConverter
    {

        private static readonly Dictionary<string, BitmapImage> _characterRaces =
     new Dictionary<string, BitmapImage>();

        //needs to be fixed
        //private static readonly Dictionary<string, string> _locations =
        //    new Dictionary<string, string>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string filename))
            {
                return null;
            }

            if (!_characterRaces.ContainsKey(filename))
            {
                _characterRaces.Add(filename,
               new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}{filename}",
                                       UriKind.Absolute)));
                // needs to be fixed
                // _locations.Add(filename,
                //                "3");
            }

            return _characterRaces[filename];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
