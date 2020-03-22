using HtmlAgilityPack;

namespace HackerNews.Interfaces
{
    public interface IPostParser
    {
        Post Parse(HtmlDocument htmlDoc, int index);
    }
}