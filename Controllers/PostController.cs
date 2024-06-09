using Microsoft.AspNetCore.Mvc;
using Share.RequestModels;
using SocialMediaApi.Entities;
using SocialMediaApi.UnitOfWork;
using SocialMediaApi.Share.ResponseModels;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using SocialMediaApi.Core;

namespace SocialMediaApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostController> _logger;

        public PostController(IUnitOfWork unitOfWork, ILogger<PostController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatePostReq req)
        {
            var userExits = await _unitOfWork.UserRepository.CheckExitsAsync(req.AuthorId);
            if (!userExits)
            {
                return Problem("Author invalid");
            }

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
                return Created(String.Empty, new SuccessObject());
            }
            
            return Problem("Create post failed");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(id);
            if (post is null)
            {
                return NotFound();
            }
            var res = post.Adapt<GetPostByIdRes>();
            return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdatePostReq req)
        {

            var post = await _unitOfWork.PostRepository.GetByIdAsync(req.PostId);
            if (post is null)
            {
                return NotFound();
            }

            post.Content = req.Title;
            post.Content = req.Content;

            _unitOfWork.PostRepository.Update(post);
            var result = await _unitOfWork.CompleteAsync();

            if (result < 0)
            {
                return Problem("Error", statusCode: StatusCodes.Status304NotModified);
            }
            return Ok(new SuccessObject());
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string id)
        {
            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult> Comment()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> ReplyComment()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> UpdateComment()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> DeleteComment()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCommentOfPost()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Like()
        {
            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult> Unlike()
        {
            return Ok();
        }
    }
}
