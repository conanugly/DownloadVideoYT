using Application.System.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DownloadSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _accountService;
        public UsersController(IUsersService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// get token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] DownloadVideoSolution.ViewModels.Account.LoginRequest request)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.Authencate(request);

            if (string.IsNullOrEmpty(result.ResultObj))
                return BadRequest(result);

            return Ok(result);

        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] ViewModels.Account.RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.RegisterUser(request);
            if(result.IsSuccessed)
                return Ok(result);

            return BadRequest(result);
        }

    }
}
