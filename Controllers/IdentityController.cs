using System.Collections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Share.RequestModels;
using SocialMediaApi.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace SocialMediaApi.Controllers
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<UserEntity> _roleManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public IdentityController(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult> SignUp([FromBody] SignUpReq req)
        {
            var user = new UserEntity
            {
                UserName = req.UserName,
                Email = req.Email,
            };

            // Tạo user sẽ tự bắt lỗi các validate
            var identityResult = await _userManager.CreateAsync(user, req.Password);
            if (!identityResult.Succeeded)
            {
                return Problem(
                    detail: identityResult.ToString(),
                    statusCode: StatusCodes.Status400BadRequest);
            }

            return Ok( new {StatusCode= StatusCodes.Status200OK});
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(
            [FromBody] SignInReq req)
        {

            _signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;

            var result = await _signInManager.PasswordSignInAsync(
                req.UserName, req.Password, isPersistent: false, lockoutOnFailure: true);

            if (result.RequiresTwoFactor)
            {
                if (!string.IsNullOrEmpty(req.TwoFactorCode))
                {
                    result = await _signInManager.TwoFactorAuthenticatorSignInAsync(
                        req.TwoFactorCode, isPersistent: false, rememberClient: false);
                }
                else if (!string.IsNullOrEmpty(req.TwoFactorRecoveryCode))
                {
                    result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(
                        req.TwoFactorRecoveryCode);
                }
            }

            if (!result.Succeeded)
            {
                return Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }

            var accessTokenResponse = new AccessTokenResponse
            {
                AccessToken = GenerateToken(),
                ExpiresIn = 10,
                RefreshToken = null
            };
            return Ok(accessTokenResponse!);
        }


        [HttpPost]
        public async Task<IdentityResult> SignOut([FromBody] SignUpReq req)
        {
            var user = new UserEntity
            {
                UserName = req.UserName,
                Email = req.Email,
            };

            // Tạo user sẽ tự bắt lỗi các validate
            var identityResult = await _userManager.CreateAsync(user, req.Password);

            if (!identityResult.Succeeded)
            {
                return IdentityResult.Failed(identityResult.Errors.ToArray());
            }

            return IdentityResult.Success;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> RefeshToken()
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


        private string GenerateToken()
        {
            var key = Encoding.ASCII.GetBytes("%SocialMediaSecret@1183002#Vietnamese%");
            var authSigningKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            var jwtHeader = new JwtHeader(signingCredentials);
            var jwtPayload = new JwtPayload();
            var token = new JwtSecurityToken(jwtHeader, jwtPayload);

            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }

        [HttpGet]
        public async Task<ActionResult> ForgotPassword()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> ConfimEmail()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> ResendConfirmEmail()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> ResetPassword()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetInfoUser()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> UpdateInfoUser()
        {
            return Ok();
        }
    }
}
