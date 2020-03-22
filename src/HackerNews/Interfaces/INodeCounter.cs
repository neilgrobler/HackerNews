using HtmlAgilityPack;

namespace HackerNews.Interfaces
{
    public interface INodeCounter
    {
        int CountNodes(HtmlDocument htmlDoc);
    }
}