using HtmlAgilityPack;

namespace HackerNews.Interfaces
{
    public interface IAuthorParser
    {
        string Parse(HtmlDocument htmlDoc, int index);
    }
}