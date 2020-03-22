﻿using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;
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

            rootCommand.Handler = CommandHandler.Create<int>(posts =>
            {
                var result = serviceProvider.GetService<IPostScraper>().ScrapeAsync(posts).Result;
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
            var serviceProvider = new ServiceCollection()
                .AddSingleton(configuration)
                .AddSingleton<IAuthorParser, AuthorParser>()
                .AddSingleton<ICommentsParser, CommentsParser>()
                .AddSingleton<INextPageUrlParser, NextPageUrlParser>()
                .AddSingleton<INodeCounter, NodeCounter>()
                .AddSingleton<IPointsParser, PointsParser>()
                .AddSingleton<IPostParser, PostParser>()
                .AddSingleton<IRankParser, RankParser>()
                .AddSingleton<ITitleParser, TitleParser>()
                .AddSingleton<IUrlParser, UrlParser>()
                .AddSingleton<IPostScraper, PostScraper>()
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}