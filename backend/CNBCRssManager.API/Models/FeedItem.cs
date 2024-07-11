namespace CNBCRssManager.API.Models
{
    public class FeedItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsRead { get; set; }
    }
}