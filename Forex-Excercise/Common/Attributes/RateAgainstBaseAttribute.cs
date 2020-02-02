using System;

namespace Exchange.Common.Attributes
{
    public sealed class RateAgainstBaseAttribute : Attribute
    {
        public double Value { get; }

        public RateAgainstBaseAttribute(double val) => Value = val;
    }
}