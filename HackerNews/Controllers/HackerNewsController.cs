using HackerNews.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HackerNews.Controllers
{
    [ApiController]
    [Route("/best20")]
    public class HackerNewsController : ControllerBase
    {
        private readonly IHackerNewsService hackerNewsService;
        private readonly IConfiguration configuration;

        public HackerNewsController(IHackerNewsService hackerNewsService, IConfiguration configuration)
        {
            this.hackerNewsService = hackerNewsService;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetTopStories()
        {
            var count = configuration.GetValue<int>(
                "HackerNewsSettings:TopStories");

            return Ok(hackerNewsService.GetTopStories(count));
        }
    }
}
