using CNBCRssManager.API.Models;

namespace CNBCRssManager.API.Repositories
{
    public interface IFeedRepository
    {
        Task<IEnumerable<FeedItem>> GetAllItemsAsync();
        Task<IEnumerable<FeedItem>> GetUnreadItemsAsync();
        Task<FeedItem> GetItemByIdAsync(int id);
        Task AddItemAsync(FeedItem item);
        Task UpdateItemAsync(FeedItem item);
        Task DeleteItemAsync(int id);
    }
}