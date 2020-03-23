using System.Threading.Tasks;

namespace HackerNews.Interfaces
{
    public interface IScraper
    {
        Task<string> ScrapeAsync(int count);
    }
}