using Microsoft.AspNetCore.Mvc;
using Share.RequestModels;
using SocialMediaApi.Entities;
using SocialMediaApi.UnitOfWork;
using SocialMediaApi.Share.ResponseModels;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using SocialMediaApi.Core;
using SocialMediaApi.Share.RequestModels;
using System.ComponentModel.Design;

namespace SocialMediaApi.Controllers
{
    [Route("Api/[controller]/[action]")]
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
        public async Task<ActionResult> Create(CreatePostReq req)
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


        [HttpGet]
        public async Task<ActionResult> GetAll(PaginationReq req)
        {
            var posts = await _unitOfWork.PostRepository.GetPostsAsync(req.PageSize, req.PageIndex);
            return Ok(posts);
        }


        [HttpGet]
        public async Task<ActionResult> Get(int id)
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
        public async Task<ActionResult> Update(UpdatePostReq req)
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
        public async Task<ActionResult> Delete(int id)
        {
            var postExits = await _unitOfWork.PostRepository.CheckExitsAsync(id);
            if (postExits)
            {
                var postEntity = await _unitOfWork.PostRepository.GetByIdAsync(id);
                postEntity!.IsDeleted = true;
                postEntity!.DeleteAt = Helper.GetUtcTimestampNow();

                _unitOfWork.PostRepository.Update(postEntity);
                await _unitOfWork.CompleteAsync();
                return Ok(new SuccessObject());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Comment(CreateCommentReq req)
        {
            var postExits = await _unitOfWork.PostRepository.CheckExitsAsync(req.PostId);
            if (!postExits)
            {
                return NotFound();
            }

            var commendEntity = new CommentEntity
            {
                Content = req.Content,
                ParentCommentId = req.ParentCommentId,
                UserId = req.UserId,
                PostId = req.PostId,
                CreatedAt = Helper.GetUtcTimestampNow()
            };

            await _unitOfWork.CommentRepository.CreateAsync(commendEntity);
            await _unitOfWork.CompleteAsync();
            return Ok(new SuccessObject());
        }

        [HttpPost]
        public async Task<ActionResult> UpdateComment(UpdateCommentReq req)
        {
            var commentEntity = await _unitOfWork.CommentRepository.GetByIdAsync(req.CommentId);
            if (commentEntity is null)
            {
                return NotFound();
            }
            commentEntity.Content = req.Content;
            _unitOfWork.CommentRepository.Update(commentEntity);
            await _unitOfWork.CompleteAsync();

            return Ok(new SuccessObject());
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteComment(int commentId)
        {
            var commentEntity = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
            if (commentEntity is null)
            {
                return NotFound();
            }
            // Delete all comment child (reply)
            _unitOfWork.CommentRepository.DeleteAllReply(commentEntity.Id);
            await _unitOfWork.CompleteAsync();

            return Ok(new SuccessObject());
        }

        [HttpGet]
        public async Task<ActionResult> GetAllByPostIdAsync(int postId)
        {
            var commentEntities = await _unitOfWork.CommentRepository.GetAllByPostIdAsync(postId);
            return Ok(commentEntities);
        }

        [HttpPost]
        public async Task<ActionResult> Like(LikeReq req)
        {
            var exitsUser = await _unitOfWork.UserRepository.CheckExitsAsync(req.UserId);
            var post = await _unitOfWork.PostRepository.GetByIdAsync(req.PostId);

            if (!exitsUser || post is null)
            {
                return NotFound();
            }

            var exitsLike = await _unitOfWork.LikeRepository.CheckExitsAsync(req.PostId, req.UserId);
            if (exitsLike)
            {
                return BadRequest();
            }

            var likeEntity = new LikeEntity
            {
                UserId = req.UserId,
                PostId = req.PostId
            };
            await _unitOfWork.LikeRepository.CreateAsync(likeEntity);

            post.LikeCount++;
            _unitOfWork.PostRepository.Update(post);

            await _unitOfWork.CompleteAsync();

            return Ok(new SuccessObject());
        }

        [HttpPost]
        public async Task<ActionResult> Unlike(LikeReq req)
        {
            var exitsUser = await _unitOfWork.UserRepository.CheckExitsAsync(req.UserId);
            var post = await _unitOfWork.PostRepository.GetByIdAsync(req.PostId);
            var exitsLikeEntity = await _unitOfWork.LikeRepository.GetByIdAsync(req.UserId, req.PostId);

            if (!exitsUser || post is null || exitsLikeEntity is null)
            {
                return NotFound();
            }

            _unitOfWork.LikeRepository.Delete(exitsLikeEntity);

            post.LikeCount--;
            _unitOfWork.PostRepository.Update(post);

            await _unitOfWork.CompleteAsync();

            return Ok(new SuccessObject());
        }
    }
}
