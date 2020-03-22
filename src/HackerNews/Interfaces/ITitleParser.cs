using HtmlAgilityPack;

namespace HackerNews.Interfaces
{
    public interface ITitleParser
    {
        string Parse(HtmlDocument htmlDoc, int index);
    }
}