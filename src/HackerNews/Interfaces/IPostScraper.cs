using System.Threading.Tasks;

namespace HackerNews.Interfaces
{
    public interface IPostScraper
    {
        Task<string> ScrapeAsync(int count);
    }
}