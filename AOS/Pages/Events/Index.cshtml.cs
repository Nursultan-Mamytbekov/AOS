using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;

namespace AOS.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Material> Material { get;set; }

        public async Task OnGetAsync()
        {
            Material = await _context.Materials
                .Include(m => m.Subject).ToListAsync();
        }

        [BindProperty]
        public int? Id { get; set; }

        public IActionResult OnPost()
        {
            if (Id == null) return NotFound();
            var file = _context.Materials.FirstOrDefault(p => p.Id == Id);
            if (file == null) return NotFound();
            return File(file.File, file.ContentType, file.FileName + file.FileExtension);
        }
    }
}
