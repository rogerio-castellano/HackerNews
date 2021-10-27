namespace HackerNews.Domain
{
    public class Story
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string PostedBy { get; set; }
        public int Time { get; set; }
        public int Score { get; set; }
        public int CommentCount { get; set; }

    }
}
