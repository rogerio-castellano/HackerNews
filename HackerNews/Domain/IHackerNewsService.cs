using HackerNews.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Domain
{
    public interface IHackerNewsService
    {
        IEnumerable<Story> GetTopStories(int count);
    }
}