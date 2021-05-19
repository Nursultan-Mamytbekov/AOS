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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ExamResultGrade> ExamResultGrade { get;set; }

        public async Task OnGetAsync()
        {
            ExamResultGrade = await _context.ExamResultGrades
                .Include(e => e.ExamResult)
                .Include(e => e.Teacher).ToListAsync();
        }
    }
}
