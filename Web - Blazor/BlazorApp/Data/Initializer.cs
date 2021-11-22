using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class Initializer
    {
        public static async Task SeedAsync(BlazorAppContext context, UserManager<IdentityUser> userManager)
        {
            await UserSeed.SeedUsersAsync(userManager);
            await CustomerSeed.SeedCustomersAsync(context);
        }
    }
}
