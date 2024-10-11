using DownloadSolution.Data.Entities;
using DownloadVideoSolution.ViewModels.Account;
using DownloadVideoSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.System.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        //private readonly IConfiguration _config;
        //public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config)
        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            //_config = config;
        }

        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            var usesr = await _userManager.FindByNameAsync(request.UserName);
            if (usesr == null) return new ApiErrorResult<string>("Account does not exists");

            var result = await _signInManager.PasswordSignInAsync(usesr, request.Password, request.RememberMe, true);

            if (!result.Succeeded)
                return new ApiErrorResult<string>("Login credentials are incorrect. Please try again.");

            var roles = await _userManager.GetRolesAsync(usesr);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,usesr.Email),
                new Claim(ClaimTypes.GivenName,usesr.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigAppSetting.Token.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(ConfigAppSetting.Token.Issuer ,//_config["Tokens:Issuer"],
                ConfigAppSetting.Token.Issuer //_config["Tokens:Issuer"]
                , claims, expires: DateTime.Now.AddHours(6)
                , signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
