using Microsoft.EntityFrameworkCore;
using CNBCRssManager.API.Models;

namespace CNBCRssManager.API.Data
{
    public class FeedDbContext : DbContext
    {
        public FeedDbContext(DbContextOptions<FeedDbContext> options) : base(options) { }

        public DbSet<FeedItem> FeedItems { get; set; }
    }
}