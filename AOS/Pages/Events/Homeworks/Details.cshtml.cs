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
    [Authorize(Roles = "teacher")]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Homework Homework { get; set; }

        [BindProperty]
        public int? Id { get; set; }

        [BindProperty]
        public int? DeleteResultId{ get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Homework = await _context.Homeworks
                .Include(h => h.Material)
                .Include(h => h.Result)
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Homework == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Id == null) return NotFound();
            var file = _context.Homeworks.Include(p => p.HomeworkFile).FirstOrDefault(p => p.Id == Id);
            if (file == null) return NotFound();
            return File(file.HomeworkFile.Data, file.ContentType, file.FileName + file.FileExtension);
        }

        public async Task<IActionResult> OnPostDeleteResultAsync()
        {
            if (DeleteResultId == null)
            {
                return NotFound("Не найдена оценка для удаления");
            }

            var result = await _context.Results.FindAsync(DeleteResultId);

            if (result != null)
            {
                _context.Results.Remove(result);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Details", new { id = result.HomeworkId });
        }
    }
}
