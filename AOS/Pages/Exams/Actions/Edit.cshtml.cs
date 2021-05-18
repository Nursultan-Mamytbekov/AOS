using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AOS.Data;

namespace AOS.Pages.Exams.Actions
{
    public class EditModel : PageModel
    {
        private readonly AOS.Data.ApplicationDbContext _context;

        public EditModel(AOS.Data.ApplicationDbContext context)
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
           ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ExamAction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamActionExists(ExamAction.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ExamActionExists(int id)
        {
            return _context.ExamActions.Any(e => e.Id == id);
        }
    }
}
