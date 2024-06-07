using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Share.RequestModels;
using SocialMediaApi.Entities;
using SocialMediaApi.UnitOfWork;
using SocialMediaApi;
using SocialMediaApi.Share.ResponseModels;
using Mapster;

namespace SocialMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<PostController> _logger;

        public PostController(IUnitOfWork unitOfWork, ILogger<PostController> logger, UserManager<UserEntity> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostReq req)
        {
            var post = new PostEntity()
            {
                AuthorId = req.AuthorId,
                Content = req.Content,
                MediaURL = string.Empty,
                Title = req.Title,
                CreateAt = Helper.GetUtcTimestampNow()
            };

            await _unitOfWork.PostRepository.CreateAsync(post);
            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(id);
            var result = await _unitOfWork.CompleteAsync();
            if (post == null)
            {
                return NotFound();
            }
            var res = post.Adapt<GetPostByIdRes>();
            return Ok(res);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdatePostReq req)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            return Ok();
        }
    }
}
