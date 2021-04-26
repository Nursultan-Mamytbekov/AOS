using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Data
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            string teacher = "teacher";
            string student = "student";
            if (await roleManager.FindByNameAsync(teacher) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(teacher));
            }
            if (await roleManager.FindByNameAsync(student) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(student));
            }
        }
    }
}
