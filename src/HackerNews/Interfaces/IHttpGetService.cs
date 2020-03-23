using System.Threading.Tasks;

namespace HackerNews.Interfaces
{
    public interface IHttpGetService
    {
        Task<string> Get(string url);
    }
}