using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoping.Model.Types.Auth;
using Shoping.Model.Wrapper;
using Shopping.Service.Interfaces;
using System.Data;
using System.Security.Claims;

namespace Shopping.Presentation.Controllers
{

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthService authService, IConfiguration configuratio)
        {
            _authService = authService;
            _configuration = configuratio;
        }
        [AllowAnonymous]
        [Route("/[controller]/SignUp")]
        [HttpPost]
        public Response<SignUpResponse> SignUp([FromBody] SignUpType user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var result =  _authService.SignUp(user);
            return new Response<SignUpResponse> (result);


        }
        [AllowAnonymous]
        [Route("/[controller]/SignIn")]
        [HttpPost]
        public Response<SignInResponse> SignIn([FromBody] SignInType user)
        {

            var result = _authService.SignIn(user);


            return new Response<SignInResponse>(result);
        }
        [AllowAnonymous]
        [Route("/[controller]/RefreshToken")]
        [HttpPost]
        public Response<RefreshTokenResponse> RefreshToken([FromBody] RefreshTokenRequest refreshToken)
        {
            var result = _authService.RefreshToken(refreshToken.RefreshToken);
            return new Response<RefreshTokenResponse>(result);
        }
        [Authorize(Roles = "User")]
        [Authorize(Roles =  "Admin")]
        [Route("/[controller]/ChangePassword")]
        [HttpPost]
        public Response<bool> ChangePassword([FromBody] RequestChangePasswordType changePassObj)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var changePassInfo = new ChangePasswordType
            {
                NewPassword = changePassObj.NewPassword,
                Password = changePassObj.OldPassword,
                UserName= identity.FindFirst("UserName").Value
            };
            return new Response<bool>(_authService.ChangePassword(changePassInfo));
        }
    }
}
