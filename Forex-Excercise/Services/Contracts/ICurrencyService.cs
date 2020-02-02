using Exchange.Entities;

namespace Exchange.Services.Contracts
{
    public interface ICurrencyService
    {
        double GetCurrencyRate(string currencyIsoPair);

        CurrencyPair ParseCurrencyPair(string currencyIsoPair);
    }
}
