using CNBCRssManager.API.Models;

namespace CNBCRssManager.API.Services
{
    public interface IRssFeedService
    {
        Task<IEnumerable<FeedItem>> GetAllItemsAsync();
        Task<IEnumerable<FeedItem>> GetUnreadItemsAsync();
        Task<FeedItem> GetItemByIdAsync(int id);
        Task RefreshFeedAsync();
        Task MarkAsReadAsync(int id);
        Task DeleteItemAsync(int id);
    }
}