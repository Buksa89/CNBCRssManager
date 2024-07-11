using Microsoft.EntityFrameworkCore;
using CNBCRssManager.API.Data;
using CNBCRssManager.API.Models;

namespace CNBCRssManager.API.Repositories
{
    public class FeedRepository : IFeedRepository
    {
        private readonly FeedDbContext _context;

        public FeedRepository(FeedDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeedItem>> GetAllItemsAsync()
        {
            return await _context.FeedItems.ToListAsync();
        }

        public async Task<IEnumerable<FeedItem>> GetUnreadItemsAsync()
        {
            return await _context.FeedItems.Where(i => !i.IsRead).ToListAsync();
        }

        public async Task<FeedItem> GetItemByIdAsync(int id)
        {
            return await _context.FeedItems.FindAsync(id);
        }

        public async Task AddItemAsync(FeedItem item)
        {
            await _context.FeedItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(FeedItem item)
        {
            _context.FeedItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _context.FeedItems.FindAsync(id);
            if (item != null)
            {
                _context.FeedItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}