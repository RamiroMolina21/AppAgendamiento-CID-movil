using System.Globalization;
using Microsoft.Maui.Graphics;

namespace Taller_3.Converters
{
    public class EstadoColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string estado)
            {
                return estado?.ToLower() switch
                {
                    "pendiente" => Color.FromArgb("#FF9800"), // Naranja
                    "finalizada" => Color.FromArgb("#4CAF50"), // Verde
                    _ => Color.FromArgb("#757575") // Gris por defecto
                };
            }
            return Color.FromArgb("#757575");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

