using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Data.Seeds
{
    public static class UserSeed
    {
        public static void Seed(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@todo.com").Result != null)
            {
                return;
            }

            var user = new IdentityUser
            {
                UserName = "admin@todo.com",
                Email = "admin@todo.com"
            };

            var result = userManager.CreateAsync(user, "AdminTodo123!").Result;
        }
    }
}
