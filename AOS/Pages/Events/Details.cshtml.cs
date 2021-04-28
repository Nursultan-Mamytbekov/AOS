using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;
using Microsoft.AspNetCore.Authorization;

namespace AOS.Pages.Events
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Material Material { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Material = await _context.Materials
                .Include(m => m.Subject).FirstOrDefaultAsync(m => m.Id == id);

            if (Material == null)
            {
                return NotFound();
            }
            return Page();
        }

        [BindProperty]
        public int? Id { get; set; }

        public IActionResult OnPost()
        {
            if (Id == null) return NotFound();
            var file = _context.Materials.Include(p => p.MaterialFile).FirstOrDefault(p => p.Id == Id);
            if (file == null) return NotFound();
            return File(file.MaterialFile.Data, file.ContentType, file.FileName + file.FileExtension);
        }
    }
}
