using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;
using Microsoft.AspNetCore.Authorization;

namespace AOS.Pages.Results.Exams
{
    [Authorize(Roles = "student")]
    public class DetailsModel : PageModel
    {
        private readonly AOS.Data.ApplicationDbContext _context;

        public DetailsModel(AOS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ExamResultGrade ExamResultGrade { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExamResultGrade = await _context.ExamResultGrades
                .Include(e => e.ExamResult)
                .Include(e => e.Teacher).FirstOrDefaultAsync(m => m.Id == id);

            if (ExamResultGrade == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
