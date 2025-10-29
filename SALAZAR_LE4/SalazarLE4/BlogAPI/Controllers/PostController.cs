using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogAPI.Models;
using BlogDataLibrary.Data;
using BlogDataLibrary.Models;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // protect all actions in this controller
    public class PostController : ControllerBase
    {
        private readonly SqlData _data;

        public PostController(SqlData data) => _data = data;

        // POST /api/Post/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PostForm model)
        {
            if (model is null || string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Body))
                return BadRequest("Title and Body are required.");

            // userId from JWT
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("id");
            if (userIdClaim is null) return Unauthorized();

            var post = new PostModel
            {
                UserId = int.Parse(userIdClaim.Value),
                Title = model.Title,
                Body = model.Body,
                DateCreated = DateTime.UtcNow
            };

            await _data.CreatePostAsync(post);
            return Ok(new { message = "created" });
        }

        // GET /api/Post/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _data.GetPostAsync(id);
            return row is null ? NotFound() : Ok(row);
        }
    }
}
