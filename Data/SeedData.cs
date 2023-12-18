using FeedbackApp.Api.Models;
using FeedbackApp.Api.Services;
using Microsoft.AspNetCore.Identity;

namespace FeedbackApp.Api.Data
{
    public class SeedData
    {
        public static async Task LoadRoles(RoleManager<Role> roleManager)
        {
            await CreateRole(roleManager, "Admin");
            await CreateRole(roleManager, "User");
        }

        private static async Task CreateRole(RoleManager<Role> roleManager, string name)
        {
            if (await roleManager.RoleExistsAsync(name))
            {
                return;
            }

            var role = new Role(name);
            var result = await roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                Console.WriteLine($"Can't create role '{name}': {string.Join(", ", result.Errors)}");
            }
        }

        public static async Task LoadUserData(UserService userService, UserManager<User> userManager, AdminSettings admin)
        {
            await CreateAdmin(userService, userManager, admin);

            var user = new User
            {
                Email = "sunandacarlstrom@knowit.se"
            };

            // var result = await userManager.CreateAsync(user, "YourSecurePassword");

            // if (result.Succeeded)
            // {
            //     await userManager.AddToRoleAsync(user, "Admin");
            // }
            // else
            // {
            //     Console.WriteLine($"Error creating user 'sunandacarlstrom@knowit.se': {string.Join(", ", result.Errors)}");
            // }

        }

        private static async Task CreateAdmin(UserService userService, UserManager<User> userManager, AdminSettings adminSettings)
        {
            if (await userService.GetUserByEmail(adminSettings.Email) != null)
            {
                return;
            }

            var admin = new User
            {
                FirstName = adminSettings.FirstName,
                LastName = adminSettings.LastName,
                Email = adminSettings.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminSettings.Password),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            await userService.CreateUser(admin);

            await userManager.AddToRolesAsync(admin, new[] { "Admin" });
        }
    }
}