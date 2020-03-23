using HackerNews.Interfaces;
using HtmlAgilityPack;

namespace HackerNews.Implementations
{
    public class NodeCounter : INodeCounter
    {
        private const string XPath = "//table[@class = 'itemlist']/tr[@class = 'athing']";

        public int CountNodes(HtmlDocument htmlDoc)
        {
            var nodes = htmlDoc.DocumentNode.SelectNodes(XPath);

            return nodes?.Count ?? 0;
        }
    }
}