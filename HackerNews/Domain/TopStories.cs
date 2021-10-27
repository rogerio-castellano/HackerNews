using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Domain
{
    public class TopStories
    {
        private readonly int MaxStories;
        private readonly Story[] _Stories;
        private int Count;
        private int MinScore = int.MaxValue;

        public Story[] Stories => _Stories.Where(s => s != null).OrderByDescending(s => s.Score).ToArray();

        public TopStories(int maxStories)
        {
            this.MaxStories = maxStories;
            this._Stories = new Story[maxStories];
        }

        public void TryAdd(Story story)
        {
            if (Count < MaxStories)
            {
                _Stories[Count++] = story;
                MinScore = story.Score < MinScore ? story.Score : MinScore;
            }
            else if (story.Score > MinScore)
            {
                for(var i = 0; i < Count; i ++)
                {
                    if (_Stories[i].Score == MinScore)
                    {
                        _Stories[i] = story;
                        MinScore = story.Score;
                    }
                }
            }
        }

    }
}
