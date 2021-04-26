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
            var file = _context.Materials.Include(p => p.File).FirstOrDefault(p => p.Id == Id);
            if (file == null) return NotFound();
            return File(file.File.Data, file.ContentType, file.FileName + file.FileExtension);
        }
    }
}
