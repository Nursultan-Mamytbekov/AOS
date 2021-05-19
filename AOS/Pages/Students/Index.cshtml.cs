using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AOS.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AOS.Pages.Students
{
    [Authorize(Roles = "teacher")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
        }

        public List<User> Users { get; set; } = new List<User>();
        public async Task OnGetAsync()
        {
            var allUsers = _userManager.Users.ToList();

            foreach (var item in allUsers)
            {
                if (await _userManager.IsInRoleAsync(item, "student"))
                {
                    Users.Add(item);
                }
            }
        }
    }
}
