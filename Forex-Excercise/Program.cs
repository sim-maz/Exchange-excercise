using Exchange.Services;
using Exchange.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Exchange
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            var services = new ServiceCollection()
                .AddScoped<ICurrencyService, CurrencyService>();

            var serviceProvider = services.BuildServiceProvider();

            var result = ConvertValue(args, serviceProvider);

            Console.WriteLine(result);
        }

        public static double ConvertValue(string[] args, ServiceProvider services)
        {
            var currencyService = services.GetService<ICurrencyService>();

            var rate = currencyService.GetCurrencyRate(args[0]);

            if (!double.TryParse(args[1], out var result))
                throw new ArgumentException("A value to convert wasn't provided.");

            return result * rate;
        }

        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            var log = new LoggerConfiguration()
                .WriteTo.File($"log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            log.Error($"{DateTime.UtcNow}: Unhandled exception encoutered. {e.ExceptionObject.ToString()}");

            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}