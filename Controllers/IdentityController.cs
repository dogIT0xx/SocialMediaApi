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
        public async Task<IdentityResult> SignUp([FromBody] SignUpReq req)
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


        [HttpPost]
        public async Task<Results<Ok<AccessTokenResponse>, ProblemHttpResult>> SignIn(
            [FromBody] SignInReq req,
            [FromQuery] bool? useCookies,
            [FromQuery] bool? useSessionCookies)
        {
            var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
            var isPersistent = (useCookies == true) && (useSessionCookies != true);
            _signInManager.AuthenticationScheme = 
                useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

            var result = await _signInManager.PasswordSignInAsync(req.UserName, req.Password, isPersistent, lockoutOnFailure: true);

            if (result.RequiresTwoFactor)
            {
                if (!string.IsNullOrEmpty(req.TwoFactorCode))
                {
                    result = await _signInManager.TwoFactorAuthenticatorSignInAsync(
                        req.TwoFactorCode, isPersistent, rememberClient: isPersistent);
                }
                else if (!string.IsNullOrEmpty(req.TwoFactorRecoveryCode))
                {
                    result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(
                        req.TwoFactorRecoveryCode);
                }
            }

            if (!result.Succeeded)
            {
                return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }

            var accessTokenResponse = new AccessTokenResponse
            {
                AccessToken = GenerateToken(),
                ExpiresIn = 10,
                RefreshToken = null
            };
            return TypedResults.Ok(accessTokenResponse!);
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
    }
}
