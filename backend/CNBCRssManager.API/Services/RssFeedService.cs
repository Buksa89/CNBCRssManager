using System.ServiceModel.Syndication;
using System.Xml;
using CNBCRssManager.API.Models;
using CNBCRssManager.API.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace CNBCRssManager.API.Services
{
    public class RssFeedService : IRssFeedService
    {
        private readonly IFeedRepository _repository;
        private readonly IMemoryCache _cache;
        private readonly IOptions<RssFeedOptions> _options;
        private readonly ILogger<RssFeedService> _logger;

        public RssFeedService(IFeedRepository repository, IMemoryCache cache, IOptions<RssFeedOptions> options, ILogger<RssFeedService> logger)
        {
            _repository = repository;
            _cache = cache;
            _options = options;
            _logger = logger;
        }

        public async Task<IEnumerable<FeedItem>?> GetAllItemsAsync()
        {
            return await _cache.GetOrCreateAsync("AllItems", async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                return await _repository.GetAllItemsAsync();
            });
        }

        public async Task<IEnumerable<FeedItem>?> GetUnreadItemsAsync()
        {
            return await _cache.GetOrCreateAsync("UnreadItems", async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                return await _repository.GetUnreadItemsAsync();
            });
        }
        public async Task<FeedItem> GetItemByIdAsync(int id)
        {
            return await _repository.GetItemByIdAsync(id);
        }

        public async Task RefreshFeedAsync()
        {
            try
            {
                using (var reader = XmlReader.Create(_options.Value.FeedUrl))
                {
                    var feed = SyndicationFeed.Load(reader);
                    foreach (var item in feed.Items)
                    {
                        var feedItem = new FeedItem
                        {
                            Title = item.Title?.Text ?? string.Empty,
                            Link = item.Links.FirstOrDefault()?.Uri?.ToString() ?? string.Empty,
                            Description = item.Summary?.Text ?? string.Empty,
                            PublishDate = item.PublishDate.DateTime,
                            IsRead = false
                        };
                        await _repository.AddItemAsync(feedItem);
                    }
                }
                _cache.Remove("AllItems");
                _cache.Remove("UnreadItems");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing RSS feed");
            }
        }

        public async Task MarkAsReadAsync(int id)
        {
            var item = await _repository.GetItemByIdAsync(id);
            if (item != null)
            {
                item.IsRead = true;
                await _repository.UpdateItemAsync(item);
                _cache.Remove("UnreadItems");
            }
        }

        public async Task DeleteItemAsync(int id)
        {
            await _repository.DeleteItemAsync(id);
            _cache.Remove("AllItems");
            _cache.Remove("UnreadItems");
        }
    }
}