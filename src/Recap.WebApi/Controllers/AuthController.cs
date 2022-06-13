using Microsoft.AspNetCore.Mvc;
using Recap.Business.Abstract;
using Recap.Entities.Dtos;

namespace Recap.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }



       

        [HttpPost]
        public ActionResult LoginEmailPassword(UserForLoginDto param)
        {
            var userToLogin = _authService.LoginEmailPassword(param);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }


    }
}
