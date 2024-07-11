using Microsoft.Extensions.Options;
using CNBCRssManager.API.Services;

namespace CNBCRssManager.API
{
    public class RssFeedBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<RssFeedOptions> _options;

        public RssFeedBackgroundService(IServiceProvider serviceProvider, IOptions<RssFeedOptions> options)
        {
            _serviceProvider = serviceProvider;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var feedService = scope.ServiceProvider.GetRequiredService<IRssFeedService>();
                    await feedService.RefreshFeedAsync();
                }
                await Task.Delay(TimeSpan.FromMinutes(_options.Value.RefreshIntervalMinutes), stoppingToken);
            }
        }
    }
}