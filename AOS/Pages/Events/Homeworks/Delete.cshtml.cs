using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;

namespace AOS.Pages.Events.Homeworks
{
    public class DeleteModel : PageModel
    {
        private readonly AOS.Data.ApplicationDbContext _context;

        public DeleteModel(AOS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Homework Homework { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Homework = await _context.Homeworks
                .Include(h => h.HomeworkFile)
                .Include(h => h.Material).FirstOrDefaultAsync(m => m.Id == id);

            if (Homework == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Homework = await _context.Homeworks.FindAsync(id);

            if (Homework != null)
            {
                _context.Homeworks.Remove(Homework);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
