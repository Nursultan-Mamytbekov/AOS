using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AOS.Data;

namespace AOS.Pages.Events.Homeworks
{
    public class EditModel : PageModel
    {
        private readonly AOS.Data.ApplicationDbContext _context;

        public EditModel(AOS.Data.ApplicationDbContext context)
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
           ViewData["HomeworkFileId"] = new SelectList(_context.HomeworkFiles, "Id", "Id");
           ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Id");
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

            _context.Attach(Homework).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeworkExists(Homework.Id))
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

        private bool HomeworkExists(int id)
        {
            return _context.Homeworks.Any(e => e.Id == id);
        }
    }
}
