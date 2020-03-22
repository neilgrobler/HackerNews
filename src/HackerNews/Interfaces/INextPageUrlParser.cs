using HtmlAgilityPack;

namespace HackerNews.Interfaces
{
    public interface INextPageUrlParser
    {
        string Parse(HtmlDocument htmlDoc);
    }
}