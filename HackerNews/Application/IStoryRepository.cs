using HackerNews.Domain;
using System.Collections.Generic;

namespace HackerNews.Application
{
    public interface IStoryRepository
    {
        IEnumerable<Story> GetBestStories();
    }
}