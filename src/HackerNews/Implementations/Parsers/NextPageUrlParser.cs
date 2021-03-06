﻿using System.Linq;
using HackerNews.Config;
using HackerNews.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;

namespace HackerNews.Implementations.Parsers
{
    public class NextPageUrlParser : INextPageUrlParser
    {
        private const string XPath = "//table[@class = 'itemlist']/.//a[@class = 'morelink']";
        private readonly string _host;

        public NextPageUrlParser(IOptions<HackerNewsOptions> options)
        {
            _host = options.Value.Host;
        }

        public string Parse(HtmlDocument htmlDoc)
        {
            var node = htmlDoc.DocumentNode.SelectSingleNode(XPath);

            if (node == null)
                return null;

            if (!node.Attributes.Contains("href"))
                return null;
            var path = node.Attributes.First(a => a.Name == "href").Value;

            return string.IsNullOrWhiteSpace(path) ? null : $"{_host}{path}";
        }
    }
}