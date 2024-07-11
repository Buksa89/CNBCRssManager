using Microsoft.AspNetCore.Mvc;
using CNBCRssManager.API.Models;
using CNBCRssManager.API.Services;

namespace CNBCRssManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedController : ControllerBase
    {
        private readonly IRssFeedService _feedService;

        public FeedController(IRssFeedService feedService)
        {
            _feedService = feedService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _feedService.GetAllItemsAsync();
            return items != null ? Ok(items) : NotFound();
        }

        [HttpGet("unread")]
        public async Task<IActionResult> GetUnreadItems()
        {
            var items = await _feedService.GetUnreadItemsAsync();
            return items != null ? Ok(items) : NotFound();
        }

        [HttpGet("item/{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _feedService.GetItemByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshFeed()
        {
            await _feedService.RefreshFeedAsync();
            return Ok();
        }

        [HttpDelete("item/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _feedService.DeleteItemAsync(id);
            return NoContent();
        }

        [HttpPut("item/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _feedService.MarkAsReadAsync(id);
            return NoContent();
        }
    }
}