using System.ServiceModel.Syndication;
using System.Xml;
using CNBCRssManager.API.Models;
using CNBCRssManager.API.Repositories;

namespace CNBCRssManager.API.Services
{
    public class RssFeedService : IRssFeedService
    {
        private readonly IFeedRepository _repository;
        private const string FeedUrl = "https://search.cnbc.com/rs/search/combinedcms/view.xml?partnerId=wrss01&id=10001147";

        public RssFeedService(IFeedRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FeedItem>> GetAllItemsAsync()
        {
            return await _repository.GetAllItemsAsync();
        }

        public async Task<IEnumerable<FeedItem>> GetUnreadItemsAsync()
        {
            return await _repository.GetUnreadItemsAsync();
        }

        public async Task<FeedItem> GetItemByIdAsync(int id)
        {
            return await _repository.GetItemByIdAsync(id);
        }

        public async Task RefreshFeedAsync()
        {
            using (var reader = XmlReader.Create(FeedUrl))
            {
                var feed = SyndicationFeed.Load(reader);
                foreach (var item in feed.Items)
                {
                    var feedItem = new FeedItem
                    {
                        Title = item.Title.Text,
                        Link = item.Links.FirstOrDefault()?.Uri.ToString(),
                        Description = item.Summary.Text,
                        PublishDate = item.PublishDate.DateTime,
                        IsRead = false
                    };
                    await _repository.AddItemAsync(feedItem);
                }
            }
        }

        public async Task MarkAsReadAsync(int id)
        {
            var item = await _repository.GetItemByIdAsync(id);
            if (item != null)
            {
                item.IsRead = true;
                await _repository.UpdateItemAsync(item);
            }
        }

        public async Task DeleteItemAsync(int id)
        {
            await _repository.DeleteItemAsync(id);
        }
    }
}