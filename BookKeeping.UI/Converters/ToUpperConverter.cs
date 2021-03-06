﻿using System;
using System.Globalization;
using System.Windows.Data;
using MahApps.Metro.Converters;

namespace BookKeeping.UI.Converters
{
    public class ToUpperConverter : MarkupConverter
    {
        public ToUpperConverter(string value)
        {
            Value = value;
        }

        public ToUpperConverter()
        {
        }

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as string;
            return val != null ? val.ToUpper() : value;
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        public string Value { get; set; }
    }
}