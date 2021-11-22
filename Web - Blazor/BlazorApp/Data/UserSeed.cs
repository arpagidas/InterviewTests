using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class UserSeed
    {
        public static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
        {
            for (int i = 1; i <= 10; i++)
            {
                string username = $"user{i}";
                string password = $"P@ssword{i}";
                if (await userManager.FindByNameAsync(username) == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = username,
                        Email = $"{username}@mail.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                    };
                    await userManager.CreateAsync(user, password);
                }
            }
        }
    }
}
