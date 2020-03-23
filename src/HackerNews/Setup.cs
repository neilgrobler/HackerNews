using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;
using HackerNews.Config;
using HackerNews.Implementations;
using HackerNews.Implementations.Parsers;
using HackerNews.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HackerNews
{
    internal class Setup
    {
        internal static void JsonSerializerFormatting()
        {
            // Set the global settings on the serializer to indented
            // and use camel case formatting
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            };
        }

        internal static RootCommand CommandLineParser(ServiceProvider serviceProvider)
        {
            // Set up a --post command that takes an integer parameter
            var argument = new Argument<int>("n");
            argument.AddValidator(result =>
            {
                var value = result.GetValueOrDefault<int>();
                return value >= 0 && value <= 100 ? null : "n must be a positive integer <= 100.";
            });

            var option = new Option<int>("--posts", "How many posts to print. A positive integer <= 100.")
            {
                Argument = argument
            };

            var rootCommand = new RootCommand("Hacker News Scraper Test");
            rootCommand.AddOption(option);

            // Handle the --post command with the IScraper service
            rootCommand.Handler = CommandHandler.Create<int>(posts =>
            {
                var result = serviceProvider.GetService<IScraper>().ScrapeAsync(posts).Result;
                Console.Write(result);
            });
            return rootCommand;
        }

        internal static ServiceProvider ConfigureServices()
        {
            // Setup the configuration
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            // Setup the container
            return new ServiceCollection()
                .AddHttpClient()
                .AddSingleton(configuration).Configure<HackerNewsOptions>(
                    configuration.GetSection("HackerNews"))
                .AddSingleton<IHttpGetService, HttpGetService>()
                .AddSingleton<IAuthorParser, AuthorParser>()
                .AddSingleton<ICommentsParser, CommentsParser>()
                .AddSingleton<INextPageUrlParser, NextPageUrlParser>()
                .AddSingleton<INodeCounter, NodeCounter>()
                .AddSingleton<IPointsParser, PointsParser>()
                .AddSingleton<IPostParser, PostParser>()
                .AddSingleton<IRankParser, RankParser>()
                .AddSingleton<ITitleParser, TitleParser>()
                .AddSingleton<IUrlParser, UrlParser>()
                .AddSingleton<IScraper, Scraper>()
                .BuildServiceProvider();
        }
    }
}
