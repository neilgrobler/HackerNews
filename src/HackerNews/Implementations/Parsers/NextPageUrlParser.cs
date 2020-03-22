using System.Linq;
using HackerNews.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;

namespace HackerNews.Implementations.Parsers
{
    public class NextPageUrlParser : INextPageUrlParser
    {
        private readonly string _host;
        private const string XPath = "//table[@class = 'itemlist']/.//a[@class = 'morelink']";

        public NextPageUrlParser(IConfiguration configuration)
        {
            _host = configuration.GetValue<string>("Host");
        }

        public string Parse(HtmlDocument htmlDoc)
        {
            var node = htmlDoc.DocumentNode.SelectSingleNode(XPath);
            
            if (node == null)
                return null;

            // Does the node have and href attribute
            if (!node.Attributes.Contains("href"))
                return null;

            // Extract the value of the attribute
            var path = node.Attributes.First(a => a.Name == "href").Value;

            return string.IsNullOrWhiteSpace(path) ? null : $"{_host}{path}";
        }
    }
}