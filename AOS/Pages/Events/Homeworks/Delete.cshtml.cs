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
    [Authorize(Roles = "student")]
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
                return NotFound("Не выбрано домашнее задание для удаления");
            }

            Homework = await _context.Homeworks
                .Include(h => h.Material)
                .Include(h => h.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Homework == null)
            {
                return NotFound("Домашнее задание было удалено");
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

            return RedirectToPage("/Events/Details", new { id = Homework.MaterialId });
        }
    }
}
