using System.IO;
using System.Threading.Tasks;
using HackerNews.Classes;
using HackerNews.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // Setup the configuration
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            // Setup the container
            var serviceProvider = new ServiceCollection()
                .Configure<HackerNewsOptions>(configuration.GetSection("hackernews"))
                .AddSingleton(configuration)
                .AddSingleton<IPostScraper, PostScraper>()
                .AddSingleton<ISerializer, JsonIndentedSerializer>()
                .AddSingleton<AppHost>()
                .BuildServiceProvider();

            // Run our host
            await serviceProvider.GetService<AppHost>().RunAsync(args);
        }
    }
}