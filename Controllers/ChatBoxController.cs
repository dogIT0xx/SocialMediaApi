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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace SocialMediaApi.Controllers
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class ChatBoxController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostController> _logger;

        public ChatBoxController(
            IUnitOfWork unitOfWork,
            ILogger<PostController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllChatBoxByUserId()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetChatBoxId()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> SendMessage()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> UnSend()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> DeleteChatBox()
        {
            return Ok();
        }
    }
}
