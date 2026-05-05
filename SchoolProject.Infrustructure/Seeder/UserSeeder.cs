using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultUser = new User
                {
                    UserName = "Admin",
                    Email = "Admin@SchoolProject.com",
                    FullName = "SchoolProject",
                    Country = "Yemen",
                    Address = "Yemen",
                    PhoneNumber = "1234567890",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                await _userManager.CreateAsync(defaultUser,"Admin1#");
                await _userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
