using Exchange.Common.Extensions;
using Exchange.Enums;

namespace Exchange.Entities
{
    public class CurrencyPair
    {
        public IsoCurrency BaseCurrency { get; set; }
        public IsoCurrency QuoteCurrency { get; set; }

        public double Rate { get { return QuoteCurrency.GetRateAgainstBase() / BaseCurrency.GetRateAgainstBase(); } }

        public CurrencyPair(IsoCurrency baseCurrency, IsoCurrency quoteCurrency)
        {
            BaseCurrency = baseCurrency;
            QuoteCurrency = quoteCurrency;
        }
    }
}