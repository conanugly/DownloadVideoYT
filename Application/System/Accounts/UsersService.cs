using DownloadSolution.Data.Entities;
using DownloadSolution.Data.Enums;
using DownloadSolution.ViewModels.Account;
using DownloadVideoSolution.ViewModels.Account;
using DownloadVideoSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.System.Accounts
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        public UsersService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            if (request.IsRole == true)
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

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigAppSetting.Authentications.Jwt.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(ConfigAppSetting.Authentications.Jwt.Issuer,
                    ConfigAppSetting.Authentications.Jwt.Issuer
                    , claims, expires: DateTime.Now.AddHours(ConfigAppSetting.Authentications.Jwt.TimeoutInMinute ?? 6)
                    , signingCredentials: creds);

                return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
            }

            if (ConfigAppSetting.Authentications.Clients.Id == request.UserName && ConfigAppSetting.Authentications.Clients.Secret == request.Password)
            {
                int expiresIn = 0;
                var token = this.GenerateJSONWebToken(request.UserName, request.Password, ref expiresIn);

                return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return new ApiErrorResult<string>("Login credentials are incorrect. Please try again.");
        }

        public async Task<ApiResult<bool>> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return new ApiErrorResult<bool>("Account does not exists");

            var res = await _userManager.DeleteAsync(user);
            if (res.Succeeded)
                return new ApiResult<bool>();
            return new ApiErrorResult<bool>("Delete Account failed");
        }

        public async Task<ApiResult<bool>> RegisterUser(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
                return new ApiErrorResult<bool>("account already exists");

            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return new ApiErrorResult<bool>("Email already exists");

            user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                Dob = request.Dob,
                Gender = (Gender)request.Gender
            };
            var res = await _userManager.CreateAsync(user, request.Password);
            if (res.Succeeded)
                return new ApiSuccessResult<bool>();
            return new ApiErrorResult<bool>("Create Account unsuccessful.");
        }

        public async Task<ApiResult<bool>> UpdateUser(UserUpdateRequest request)
        {
            var user = new AppUser();
            if (!string.IsNullOrEmpty(request.Email))
            {
                if (await _userManager.Users.AnyAsync(x => x.Id == request.Id && x.Email != request.Email))
                    return new ApiErrorResult<bool>("Email already exists");
                user.Email = request.Email;
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
                user.PhoneNumber = request.PhoneNumber;

            if (!string.IsNullOrEmpty(request.FirstName))
                user.FirstName = request.FirstName;

            if (!string.IsNullOrEmpty(request.LastName))
                user.LastName = request.LastName;

            if (request.Dob != null)
                user.Dob = (DateTime)request.Dob;

            if (request.Gender != null)
                user.Gender = (Gender)request.Gender;

            var res = await _userManager.UpdateAsync(user);
            if (res.Succeeded) return new ApiSuccessResult<bool>();
            return new ApiErrorResult<bool>("Account update failed");
        }

        #region private
        private bool VerifyClient(string client_id, string client_secret)
        {
            if (string.IsNullOrEmpty(client_id)
                || string.IsNullOrEmpty(client_secret))
                return false;
            return (client_secret == ConfigAppSetting.Authentications.Clients.Id);
        }

        private JwtSecurityToken GenerateJSONWebToken(string client_id, string client_secret, ref int expiresIn)
        {
            
            int timeoutInMinute = ConfigAppSetting.Authentications.Jwt.TimeoutInMinute ?? 6;

            expiresIn = timeoutInMinute;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,client_id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,new DateTimeOffset(DateTime.Now.AddHours(timeoutInMinute)).ToUnixTimeSeconds().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigAppSetting.Authentications.Jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(ConfigAppSetting.Authentications.Jwt.Issuer,
                ConfigAppSetting.Authentications.Jwt.Issuer
                , claims, expires: DateTime.Now.AddHours(ConfigAppSetting.Authentications.Jwt.TimeoutInMinute ?? 6)
                , signingCredentials: creds);

            return token;
        }
        #endregion
    }
}
