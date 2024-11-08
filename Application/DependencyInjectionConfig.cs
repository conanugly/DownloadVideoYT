using Application.System.Accounts;
using DownloadSolution.Application.System.Menu;
using DownloadSolution.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using DownloadSolution.ViewModels.Menu;
using Microsoft.AspNetCore.Builder;

namespace DownloadSolution.Application
{
    public class DependencyInjectionConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            //DI
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IMenuService, MenuService>();
            //

            //add IIS
            services.Configure<IISServerOptions>(op =>
            {
                op.AutomaticAuthentication = false;
            });
            //

            // AutoMaper
            services.AddAutoMapper(typeof(AutoMapperSetup));
        }
    }

    public class AutoMapperSetup : Profile
    {
        private static bool _isInitialized = false;
        private static readonly object _locker = new object();

        public AutoMapperSetup()
        {
            lock (_locker)
            {
                if (!_isInitialized)
                {
                    _isInitialized = true;
                    Setup();
                }
            }
        }

        private void Setup()
        {
            CreateMap<MenuRequest, Menu>().ReverseMap();
            CreateMap<MenuVm, Menu>().ReverseMap();
        }
    }
}