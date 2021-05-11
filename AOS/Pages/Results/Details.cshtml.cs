using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;

namespace AOS.Pages.Results
{
    public class DetailsModel : PageModel
    {
        private readonly AOS.Data.ApplicationDbContext _context;

        public DetailsModel(AOS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Result Result { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound("Не указана домашняя работа");
            }

            Result = await _context.Results
                .Include(r => r.Homework)
                .Include(r => r.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Result == null)
            {
                return NotFound("Отсутствует оценка данной домашней работы");
            }
            return Page();
        }
    }
}
