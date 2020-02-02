using Exchange.Common.Attributes;
using System;
using System.Reflection;

namespace Exchange.Common.Extensions
{
    public static class EnumExtensions
    {
        public static double GetRateAgainstBase(this Enum element)
        {
            FieldInfo fi = element.GetType().GetField(element.ToString());

            RateAgainstBaseAttribute[] attributes = (RateAgainstBaseAttribute[])fi.GetCustomAttributes(typeof(RateAgainstBaseAttribute), false);

            return attributes[0].Value;
        }
    }
}