using HtmlAgilityPack;

namespace HackerNews.Interfaces
{
    public interface IPointsParser
    {
        int Parse(HtmlDocument htmlDoc, int index);
    }
}