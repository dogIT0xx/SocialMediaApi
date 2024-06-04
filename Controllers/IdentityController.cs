using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NuGet.Protocol;
using Share.RequestModels;
using SocialMediaApi.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace SocialMediaApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<UserEntity> _roleManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public IdentityController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpReq req)
        {
            var user = new UserEntity();
            var emailValidate = await _userManager.SetEmailAsync(user, req.Email);
            var userNameValidate = await _userManager.SetUserNameAsync(user, req.UserName);

            if (emailValidate != IdentityResult.Success)
            {
                return ValidationProblem(emailValidate.ToString());
            }

            if (userNameValidate != IdentityResult.Success)
            {
                return ValidationProblem(userNameValidate.ToString());
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetToken()
        {

            var key = Encoding.ASCII.GetBytes("%SocialMediaSecret@1183002#Vietnamese%");

            var authSigningKey = new SymmetricSecurityKey(key);

            var handler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            };
            var token = handler.CreateToken(tokenDescriptor);
            return Ok(handler.WriteToken(token));
        }

        [HttpGet]
        public async Task<IActionResult> GenerateToken()
        {
            var key = Encoding.ASCII.GetBytes("%SocialMediaSecret@1183002#Vietnamese%");
            var authSigningKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);
            var jwtHeader = new JwtHeader(signingCredentials);
            
            var jwtPayload = new JwtPayload();

            var token = new JwtSecurityToken(jwtHeader, jwtPayload);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
