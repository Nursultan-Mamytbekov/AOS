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

        public async Task<IActionResult> OnGetBySubjectAsync(int? id)
        {
            if (id == null) return NotFound();

            Material = await _context.Materials
                .Include(m => m.Subject).Where(m => m.SubjectId == id).ToListAsync();

            return Page();
        }
    }
}
