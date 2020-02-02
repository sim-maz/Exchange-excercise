using Exchange.Common.Attributes;

namespace Exchange.Enums
{
    public enum IsoCurrency
    {
        [RateAgainstBase(1)]
        DKK,

        [RateAgainstBase(0.134419)]
        EUR,

        [RateAgainstBase(0.150805)]
        USD,

        [RateAgainstBase(0.117254)]
        GBP,

        [RateAgainstBase(1.314060)]
        SEK,

        [RateAgainstBase(1.275510)]
        NOK,

        [RateAgainstBase(0.146289)]
        CHF,

        [RateAgainstBase(16.739203)]
        JPY
    }
}