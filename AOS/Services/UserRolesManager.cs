using AOS.Data;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Services
{
    public class UserRolesManager : IUserRolesManager
    {
        private readonly UserManager<User> _userManager;

        public UserRolesManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> IsTeacher(ClaimsPrincipal username)
        {
            var user = await _userManager.GetUserAsync(username);
            return await _userManager.IsInRoleAsync(user, "teacher");
        }

        public async Task<bool> IsStudent(ClaimsPrincipal username)
        {
            var user = await _userManager.GetUserAsync(username);
            return await _userManager.IsInRoleAsync(user, "student");
        }
    }
}
