using Exchange.Common.Extensions;
using Exchange.Enums;
using Xunit;

namespace Exchange.tests.Common.Extensions
{
    public class EnumExtensionsTests
    {
        [Theory]
        [InlineData(IsoCurrency.CHF, 0.146289)]
        [InlineData(IsoCurrency.DKK, 1)]
        [InlineData(IsoCurrency.EUR, 0.134419)]
        [InlineData(IsoCurrency.GBP, 0.117254)]
        [InlineData(IsoCurrency.SEK, 1.314060)]
        [InlineData(IsoCurrency.NOK, 1.275510)]
        [InlineData(IsoCurrency.JPY, 16.739203)]
        [InlineData(IsoCurrency.USD, 0.150805)]
        public void GetRateAgainstBase_CurrencyEnumProvidedWithAttribute_ReturnsTheCorrectRateAgainstBaseValue(IsoCurrency currency, double expectedRate)
        {
            var resultRate = currency.GetRateAgainstBase();

            Assert.Equal(expectedRate, resultRate);
        }
    }
}
