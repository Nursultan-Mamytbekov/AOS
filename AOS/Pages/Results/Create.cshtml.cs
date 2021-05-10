using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AOS.Data;

namespace AOS.Pages.Results
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Result Result { get; set; }
        public Homework Homework { get; set; }

        public IActionResult OnGet(int? homeworkId)
        {
            if (homeworkId == null)
            {
                return NotFound("Не выбрана домашняя работа");
            }

            Homework = _context.Homeworks.FirstOrDefault(p => p.Id == homeworkId);

            if (Homework == null)
            {
                return NotFound("Не найдена домашняя работа");
            }
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Results.Add(Result);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Events/Homeworks/Index", new { id = Homework.MaterialId });
        }
    }
}
