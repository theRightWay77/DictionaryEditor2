using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace OnlineShop.Db
{
    public class IdentityInitializer
    {
        public static void Initialize(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@mail.ru";
            var password = "marMAR!123";
            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
            }
            if (roleManager.FindByNameAsync("User").Result == null)
            {
                roleManager.CreateAsync(new IdentityRole("User")).Wait();
            }
            if (roleManager.FindByNameAsync("Redactor").Result == null)
            {
                roleManager.CreateAsync(new IdentityRole("Redactor")).Wait();
            }
            if (roleManager.FindByNameAsync(adminEmail).Result == null)
            {
                var admin = new IdentityUser { Email = adminEmail, UserName = adminEmail };
                var result = userManager.CreateAsync(admin, password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, "Admin").Wait();
                }
            }
        }
    }
}
