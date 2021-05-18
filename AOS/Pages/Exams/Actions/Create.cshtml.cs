using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AOS.Data;

namespace AOS.Pages.Exams.Actions
{
    public class CreateModel : PageModel
    {
        private readonly AOS.Data.ApplicationDbContext _context;

        public CreateModel(AOS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ExamAction ExamAction { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ExamActions.Add(ExamAction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
