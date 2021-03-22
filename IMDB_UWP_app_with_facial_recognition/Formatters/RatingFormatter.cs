﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace IMDB_UWP_app_with_facial_recognition.Formatters
{
    class RatingFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return $"Rating: {(string) value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
