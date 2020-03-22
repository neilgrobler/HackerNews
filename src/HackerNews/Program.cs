using System.CommandLine;
using System.Threading.Tasks;

namespace HackerNews
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // Configure the services - DI and configuration
            var serviceProvider = Setup.ConfigureServices();

            // Set up the formatting on the serializer
            Setup.JsonSerializerFormatting();

            // Set up the command line parser
            var rootCommand = Setup.CommandLineParser(serviceProvider);

            // Parse the incoming args and invoke the handler
            await rootCommand.InvokeAsync(args);
        }
    }
}