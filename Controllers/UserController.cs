using Microsoft.AspNetCore.Mvc;
using Share.RequestModels;
using SocialMediaAPI.UnitOfWork;

namespace SocialMediaAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserController> _logger;

        public UserController(IUnitOfWork unitOfWork, ILogger<UserController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost("/SignUp")]
        public async Task<IResult> SignUp(SignUpReq req)
        {
            return Results.Problem();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn()
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Refresh()
        {
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail()
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(int i)
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> ResendConfirmEmail()
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword()
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Manage2Fa()
        {
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Manage2Info(int i)
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Manage2Info()
        {
            return NoContent();
        }
    }
}
