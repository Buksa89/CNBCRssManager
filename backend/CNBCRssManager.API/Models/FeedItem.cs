namespace CNBCRssManager.API.Models
{
    public class FeedItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public bool IsRead { get; set; }
    }
}