using HtmlAgilityPack;

namespace HackerNews.Interfaces
{
    public interface ICommentsParser
    {
        int Parse(HtmlDocument htmlDoc, int index);
    }
}