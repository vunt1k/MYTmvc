using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MYT.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYT.Data.AppContext
{
    public static class MyIdentityDataInitializer
    {
        public static void SeedData(UserManager<DbUser> userManager, RoleManager<DbRole> roleManager, AppDbContext context)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager, context);
        }        

        public static void SeedUsers(UserManager<DbUser> userManager, AppDbContext context)
        {
            List<UserProfile> userProfiles = new List<UserProfile>
            {
                new UserProfile
                {
                    Name = "Vitaliy",
                    RegistrationDate = DateTime.Now
                },
                new UserProfile
                {
                    Name = "Vuntik",
                    RegistrationDate = DateTime.Now
                }
            };

            if (userManager.FindByNameAsync("user1").Result == null)
            {
                DbUser user = new DbUser();
                user.UserName = "user1";
                user.Email = "user1@localhost";
                user.UserProfile = userProfiles[0];

                var result = userManager.CreateAsync(user, "Qwerty-1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,"User").Wait();                    
                }
            }


            if (userManager.FindByNameAsync("user2").Result == null)
            {
                DbUser user = new DbUser();
                user.UserName = "user2";
                user.Email = "user2@localhost";
                user.UserProfile = userProfiles[1];

                var result = userManager.CreateAsync(user, "Qwerty-1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,"Administrator").Wait();                    
                }
            }
        }

        public static void SeedRoles(RoleManager<DbRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                DbRole role = new DbRole();
                role.Name = "User";
                role.Description = "Perform normal operations.";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                DbRole role = new DbRole();
                role.Name = "Administrator";
                role.Description = "Perform all the operations.";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}

