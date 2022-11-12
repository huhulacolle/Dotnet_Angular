using Dotnet_Angular.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_Angular.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository postsRepository;

        public PostsController(IPostsRepository postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posts>>> GetPostAsync()
        {
            var result = await postsRepository.GetPostsAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostPostsAsync(Posts posts)
        {
            try
            {
                if (string.IsNullOrEmpty(posts.Content))
                {
                    using var client = new HttpClient();
                    posts.Content = await client.GetStringAsync("https://loripsum.net/api/plaintext");
                }
                await postsRepository.PostPostsAsync(posts);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("exemple")]
        public async Task<IActionResult> PostPostsAsyncDict(string title, string content)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                {
                    using var client = new HttpClient();
                    content = await client.GetStringAsync("https://loripsum.net/api/plaintext");
                }
                await postsRepository.PostPostsAsyncDict(title, content);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
