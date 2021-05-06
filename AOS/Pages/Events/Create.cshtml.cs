using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AOS.Data;
using AOS.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace AOS.Pages.Events
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CreateModel(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public MaterialViewModel Material { get; set; }


        public IActionResult OnGet()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var material = new Material()
            {
                FileName = Path.GetFileNameWithoutExtension(Material.File.FileName),
                FileExtension = Path.GetExtension(Material.File.FileName),
                IsActive = Material.IsActive,
                SubjectId = Material.SubjectId,
                DeadLine = Material.DeadLine,
                ContentType = Material.File.ContentType,
                User = await GetCurrentUser()
            };

            using (var reader = new BinaryReader(Material.File.OpenReadStream()))
            {
                material.MaterialFile = new MaterialFile
                {
                    Data = reader.ReadBytes((int)Material.File.Length)
                };
            }

            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private async Task<User> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }
    }
}
