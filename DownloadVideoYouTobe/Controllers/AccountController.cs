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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

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
    }
}
