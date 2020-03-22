using HtmlAgilityPack;

namespace HackerNews.Interfaces
{
    public interface IUrlParser
    {
        string Parse(HtmlDocument htmlDoc, int index);
    }
}