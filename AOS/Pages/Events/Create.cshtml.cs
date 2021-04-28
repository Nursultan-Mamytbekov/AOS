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

namespace AOS.Pages.Events
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public MaterialViewModel Material { get; set; }

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

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
                ContentType = Material.File.ContentType
            };

            using (var reader = new BinaryReader(Material.File.OpenReadStream()))
            {
                material.MaterialFile = new AOS.Data.MaterialFile
                {
                    Data = reader.ReadBytes((int)Material.File.Length)
                };
            }

            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
