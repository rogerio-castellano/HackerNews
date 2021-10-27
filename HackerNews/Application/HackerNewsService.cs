using HackerNews.Domain;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace HackerNews.Application
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly IStoryRepository storyRepository;
        private readonly IMemoryCache memoryCache;
        private readonly IConfiguration configuration;
        private int Count;

        public HackerNewsService(IStoryRepository storyRepository, IMemoryCache memoryCache, IConfiguration configuration)
        {
            this.storyRepository = storyRepository;
            this.memoryCache = memoryCache;
            this.configuration = configuration;
        }

        public IEnumerable<Story> GetTopStories(int count)
        {
            Count = count;
            Story[] topStories;

            if (CachedTopStoryExists(out topStories))
            {
                var expirationTime = configuration.GetValue<int>(
                "HackerNewsSettings:HackerNewsExpirationTimeInSeconds");
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(expirationTime));
                topStories = GetTopStories();

                memoryCache.Set(CacheKeys.TopStories, topStories, cacheEntryOptions);
            }

            return topStories;
        }

        private Story[] GetTopStories()
        {
            var stories = new TopStories(Count);

            foreach (var story in storyRepository.GetBestStories())
            {
                stories.TryAdd(story);
            }

            return stories.Stories;
        }

        private bool CachedTopStoryExists(out Story[] topStories)
        {
            return !memoryCache.TryGetValue(CacheKeys.TopStories, out topStories);
        }
    }
}
