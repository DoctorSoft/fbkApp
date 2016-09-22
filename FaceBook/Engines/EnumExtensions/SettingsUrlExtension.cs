using System;
using System.ComponentModel;
using System.Reflection;
using Engines.Enums;

namespace Engines.EnumExtensions
{
    public static class SettingsUrlExtension
    {
        public static string GetDiscription(this SettingsUrl value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = 
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
