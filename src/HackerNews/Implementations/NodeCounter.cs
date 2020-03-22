using HackerNews.Interfaces;
using HtmlAgilityPack;

namespace HackerNews.Implementations
{
    public class NodeCounter : INodeCounter
    {
        private const string XPath = "//table[@class = 'itemlist']/tr[@class = 'athing']";

        public int CountNodes(HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.SelectNodes(XPath).Count;
        }
    }
}