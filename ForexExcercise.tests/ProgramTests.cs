using ApprovalTests.Reporters;
using Exchange.Services;
using Exchange.Services.Contracts;
using Moq;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace Exchange.tests
{
    [UseReporter(typeof(DiffReporter))]
    public class ProgramTests
    {
        private readonly Mock<IServiceProvider> _serviceProvider;
        private readonly ICurrencyService _currencyService;

        public ProgramTests()
        {
            _serviceProvider = new Mock<IServiceProvider>();

            _currencyService = new CurrencyService(_serviceProvider.Object);
        }

        [Theory]
        [InlineData("EUR/DKK", "150", "1115.91")]
        [InlineData("DKK/DKK", "60", "60")]
        [InlineData("EUR/USD", "20", "22.438")]
        [InlineData("JPY/CHF", "2000", "17.4")]
        [InlineData("CHF/JPY", "2000", "228851.2")]
        public void ConvertValue_ValidArgumentProvided_ConsoleOutputCorrect(string args0, string args1, string expectedResult)
        {
            string[] args = new string[] { args0, args1 };

            StringBuilder fakeoutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeoutput));
            Program.Main(args);

            var output = fakeoutput.ToString().Trim();
            Assert.Equal(expectedResult.ToString(), output);
        }
    }
}