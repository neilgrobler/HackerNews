using HtmlAgilityPack;

namespace HackerNews.Interfaces
{
    public interface IRankParser
    {
        int Parse(HtmlDocument htmlDoc, int index);
    }
}