using System;
using HackerNews.Interfaces;
using HtmlAgilityPack;

namespace HackerNews.Implementations.Parsers
{
    public class UrlParser : IUrlParser
    {
        private const string XPath = "//table[@class='itemlist']/tr[@class='athing'][{0}]//a[@class='storylink']";
        private const string DefaultValue = "";

        public string Parse(HtmlDocument htmlDoc, int index)
        {
            var xpath = string.Format(XPath, index);
            var node = htmlDoc.DocumentNode.SelectSingleNode(xpath);

            if (node == null || !node.Attributes.Contains("href"))
                return DefaultValue;

            return Uri.TryCreate(node.Attributes["href"].Value, UriKind.Absolute, out var uri)
                ? uri.ToString()
                : DefaultValue;
        }
    }
}