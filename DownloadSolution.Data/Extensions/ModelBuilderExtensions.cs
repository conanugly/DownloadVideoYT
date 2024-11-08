using DownloadSolution.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadSolution.Data.Extensions
{
    internal static class ModelBuilderExtensions
    {
        internal static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig() { Key = "HomeTitle", Value = "This is home page of DownloadVideo" },
               new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of DownloadVideo" },
               new AppConfig() { Key = "HomeDescription", Value = "This is description of DownloadVideo" }
                );

            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi", IsDefault = true, Name = "Tiếng Việt" },
                new Language() { Id = "en", IsDefault = false, Name = "English" }
                );

            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");

            modelBuilder.Entity<AppRole>().HasData(new AppRole()
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Admin role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser()
            {
                Id = adminId,
                UserName = "admin",
                Email = "truongnn@ominext.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Truong",
                LastName = "Truong",
                Dob = new DateTime(1999, 03, 02),
                Gender = Enums.Gender.Male
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

            /*modelBuilder.Entity<Menu>().HasData(
                new Menu()
                {
                    
                    Name = "Home",
                    Description = "Home page",
                    Icon = "home",
                    CreateBy = adminId,
                    CreatedDate = DateTime.Now
                },
                new Menu()
                {
                    Name = "Contact",
                    Description = "Contact us",
                    Icon = "contacts",
                    CreateBy = adminId,
                    CreatedDate = DateTime.Now
                },
                new Menu()
                {
                    Name = "About",
                    Description = "Our information",
                    Icon = "perm_contact_calendar",
                    CreateBy = adminId,
                    CreatedDate = DateTime.Now
                }
            );*/
        }
    }
}
