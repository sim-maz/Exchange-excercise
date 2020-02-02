using Exchange.Entities;
using Exchange.Enums;
using Exchange.Services.Contracts;
using System;

namespace Exchange.Services
{
    public class CurrencyService : ICurrencyService
    {
        public CurrencyService(IServiceProvider services)
        {
        }

        public double GetCurrencyRate(string currencyIsoPair)
        {
            var currencyPair = ParseCurrencyPair(currencyIsoPair);

            return Math.Round(currencyPair.Rate, 4);
        }

        public CurrencyPair ParseCurrencyPair(string currencyIsoPair)
        {
            var inputs = currencyIsoPair.Split('/');

            if (inputs.Length != 2)
                throw new ArgumentNullException("Currency pair format unrecognized.");

            var baseCurrencyInput = inputs[0];
            var quoteCurrencyInput = inputs[1];

            if (!Enum.TryParse<IsoCurrency>(baseCurrencyInput, out var baseCurrencyIso))
                throw new ArgumentException("Couldn't recognize the provided base currency code.");

            if (!Enum.TryParse<IsoCurrency>(quoteCurrencyInput, out var quoteCurrencyIso))
                throw new ArgumentException("Couldn't recognize the provided quote currency code.");

            return new CurrencyPair(baseCurrencyIso, quoteCurrencyIso);
        }
    }
}