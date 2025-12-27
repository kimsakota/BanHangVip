using System.Globalization;
using Microsoft.Maui.Controls;

namespace BanHangVip.Converters
{
    // Converter chuyển đổi Type sang Màu nền
    public class TypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value as string;
            return type switch
            {
                "INTAKE" => Colors.Orange,   // Màu cam cho nhập hàng
                "DELIVERY" => Colors.Green,  // Màu xanh lá cho giao hàng
                "PAYMENT" => Colors.Purple,  // Màu tím cho thanh toán
                _ => Colors.Gray                  // Mặc định
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Converter chuyển đổi Type sang Icon (Emoji hoặc Text)
    public class TypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value as string;
            return type switch
            {
                "INTAKE" => "📥",     // Icon nhập hàng
                "DELIVERY" => "🚚",   // Icon giao hàng
                "PAYMENT" => "💰",    // Icon thanh toán
                _ => "❓"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}