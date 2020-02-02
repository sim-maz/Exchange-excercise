using Exchange.Entities;
using Exchange.Services;
using Exchange.Services.Contracts;
using Moq;
using System;
using Xunit;

namespace Exchange.tests.Services
{
    public class CurrencyServiceTests
    {
        private readonly Mock<IServiceProvider> _serviceProvider;
        private readonly ICurrencyService _currencyService;

        public CurrencyServiceTests()
        {
            _serviceProvider = new Mock<IServiceProvider>();

            _currencyService = new CurrencyService(_serviceProvider.Object);
        }


        [Theory]
        [InlineData("EUR/DKK", 7.4394)]
        [InlineData("USD/DKK", 6.6311)]
        [InlineData("GBP/DKK", 8.5285)]
        [InlineData("SEK/DKK", 0.7610)]
        [InlineData("NOK/DKK", 0.7840)]
        [InlineData("CHF/DKK", 6.8358)]
        [InlineData("JPY/DKK", 0.0597)]
        public void GetCurrencyRate_CurrencyPairExists_ReturnCorrectRate(string currencyIsoPair, double expectedExchangeRate)
        {
            double exchangeRate = _currencyService.GetCurrencyRate(currencyIsoPair);

            Assert.Equal(expectedExchangeRate, exchangeRate);
        }

        [Theory]
        [InlineData("EUR/DKK", "EUR", "DKK")]
        [InlineData("USD/EUR", "USD", "EUR")]
        [InlineData("EUR/USD", "EUR", "USD")]
        [InlineData("DKK/JPY", "DKK", "JPY")]
        [InlineData("GBP/DKK", "GBP", "DKK")]
        [InlineData("SEK/DKK", "SEK", "DKK")]
        [InlineData("NOK/DKK", "NOK", "DKK")]
        [InlineData("CHF/DKK", "CHF", "DKK")]
        [InlineData("JPY/DKK", "JPY", "DKK")]
        public void ParseCurrencyPair_CurrencyPairValid_ReturnCurrencyTuple(string currencyIsoPair, string expectedBaseCurrency, string expectedQuoteCurrency)
        {
            CurrencyPair resultPair = _currencyService.ParseCurrencyPair(currencyIsoPair);

            Assert.Equal(expectedBaseCurrency, resultPair.BaseCurrency.ToString());
            Assert.Equal(expectedQuoteCurrency, resultPair.QuoteCurrency.ToString());
        }

        [Theory]
        [InlineData("ZZZ/DKK", "Couldn't recognize the provided base currency code.")]
        [InlineData("ZZZZ/ZZZ", "Couldn't recognize the provided base currency code.")]
        [InlineData("USD/ZZZ", "Couldn't recognize the provided quote currency code.")]
        public void Currency_Pair_Provided_Is_Invalid_Throws_ArgumentException(string currencyIsoPair, string expectedMessage)
        {
            Exception ex = Assert.Throws<ArgumentException>(() => _currencyService.ParseCurrencyPair(currencyIsoPair));

            Assert.Equal(expectedMessage, ex.Message);
        }

        [Theory]
        [InlineData("JPY-DKK", "Value cannot be null. (Parameter 'Currency pair format unrecognized.')")]
        [InlineData("JPY.DKK", "Value cannot be null. (Parameter 'Currency pair format unrecognized.')")]
        [InlineData("Lorem ipsum", "Value cannot be null. (Parameter 'Currency pair format unrecognized.')")]
        public void Input_Text_Is_Invalid_Throws_ArgumentNullException(string currencyIsoPair, string expectedMessage)
        {
            Exception ex = Assert.Throws<ArgumentNullException>(() => _currencyService.ParseCurrencyPair(currencyIsoPair));

            Assert.Equal(expectedMessage, ex.Message);
        }
    }
}
