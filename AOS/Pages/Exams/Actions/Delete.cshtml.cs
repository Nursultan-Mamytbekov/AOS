using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;

namespace AOS.Pages.Exams.Actions
{
    public class DeleteModel : PageModel
    {
        private readonly AOS.Data.ApplicationDbContext _context;

        public DeleteModel(AOS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ExamAction ExamAction { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExamAction = await _context.ExamActions
                .Include(e => e.Exam).FirstOrDefaultAsync(m => m.Id == id);

            if (ExamAction == null)
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

            ExamAction = await _context.ExamActions.FindAsync(id);

            if (ExamAction != null)
            {
                _context.ExamActions.Remove(ExamAction);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
