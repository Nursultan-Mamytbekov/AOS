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
    public class IndexModel : PageModel
    {
        private readonly AOS.Data.ApplicationDbContext _context;

        public IndexModel(AOS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ExamAction> ExamAction { get;set; }

        public async Task OnGetAsync()
        {
            ExamAction = await _context.ExamActions
                .Include(e => e.Exam).ToListAsync();
        }
    }
}
