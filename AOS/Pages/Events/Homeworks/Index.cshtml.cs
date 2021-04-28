using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;
using Microsoft.AspNetCore.Authorization;

namespace AOS.Pages.Events.Homeworks
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Homework> Homework { get;set; }

        public async Task OnGetAsync()
        {
            Homework = await _context.Homeworks
                .Include(h => h.HomeworkFile)
                .Include(h => h.Material).ToListAsync();
        }

        public async Task<IActionResult> OnGetByMaterialAsync(int? id)
        {
            if (id == null) return NotFound();

            Homework = await _context.Homeworks
                .Include(h => h.Material).Where(h => h.MaterialId == id).ToListAsync();

            return Page();
        }

        [BindProperty]
        public int? Id { get; set; }

        public IActionResult OnPost()
        {
            if (Id == null) return NotFound();
            var file = _context.Homeworks.Include(p => p.HomeworkFile).FirstOrDefault(p => p.Id == Id);
            if (file == null) return NotFound();
            return File(file.HomeworkFile.Data, file.ContentType, file.FileName + file.FileExtension);
        }
    }
}
