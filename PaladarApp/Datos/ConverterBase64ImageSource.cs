using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PaladarApp.Datos
{
    class ConverterBase64ImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] Foto = (byte[])value;

            if (Foto == null)
                return null;

            // Convert base64Image from string to byte-array
            string fotof = System.Convert.ToBase64String(Foto);
            var imageBytes = System.Convert.FromBase64String(fotof);

            // Return a new ImageSource
            return ImageSource.FromStream(() => { return new MemoryStream(imageBytes); });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
