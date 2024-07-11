namespace CNBCRssManager.API
{
    public class RssFeedOptions
    {
        public string FeedUrl { get; set; } = string.Empty;
        public int RefreshIntervalMinutes { get; set; }
    }
}