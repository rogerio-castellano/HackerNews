using HackerNews.Application;
using HackerNews.Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace HackerNews.Data
{
    public class StoryRepository : IStoryRepository
    {
        private readonly IConfiguration configuration;

        public StoryRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<Story> GetBestStories()
        {
            var allStories = new List<Story>();
            var baseUrlHackerNews = configuration.GetValue<string>(
                "HackerNewsSettings:BaseUrlHackerNews");

            using (var hc = new HttpClient())
            {
                var bestStoriesList = hc.GetStringAsync($"{baseUrlHackerNews}/v0/beststories.json").Result;
                var bestStoriesId = JsonConvert.DeserializeObject<int[]>(bestStoriesList);

                foreach (int storyId in bestStoriesId)
                {
                    var story = hc.GetStringAsync($"{baseUrlHackerNews}/v0/item/{storyId}.json").Result;
                    allStories.Add(JsonConvert.DeserializeObject<Story>(story));
                }
            }

            return allStories;
        }
    }
}
