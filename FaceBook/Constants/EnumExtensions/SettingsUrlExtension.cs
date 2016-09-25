using System;
using System.ComponentModel;
using System.Reflection;

namespace Constants.EnumExtensions
{
    public static class SettingsUrlExtension
    {
        public static string GetDiscription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
