using System;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews
{
    public class AppHost
    {
        private readonly IServiceProvider _serviceProvider;

        public AppHost(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task RunAsync(string[] args)
        {
            // System.CommandLine
            if (args.Any())
                switch (args[0].ToLower())
                {
                    case "--posts":
                        // Check the the second parameter is present and an integer between 0 and 100.
                        if (args.Length < 2 || !int.TryParse(args[1], out var count))
                        {
                            Console.Write(
                                "ERROR: The number of posts to return must be supplied as an integer between 0 and 100.");
                            return;
                        }

                        // Get the posts
                        var output = await _serviceProvider.GetService<IPostScraper>()
                            .ScrapeAsync(count);

                        // Serialize the the data
                        Console.Write(output);
                        return;
                    default:
                        Console.Write("ERROR: Invalid command");
                        return;
                }

            Console.Write("ERROR: No command supplied.");
        }
    }
}