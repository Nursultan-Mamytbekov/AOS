using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;

namespace AOS.Pages.Exams.Tickets
{
    public class IndexModel : PageModel
    {
        private readonly AOS.Data.ApplicationDbContext _context;

        public IndexModel(AOS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Ticket> Tickets { get;set; }

        public Exam Exam { get; set; }

        public async Task OnGetAsync(int? examId)
        {
            Exam = await _context.Exams.FirstOrDefaultAsync(p => p.Id == examId);
            Tickets = await _context.Tickets
                .Include(t => t.Exam)
                .Where(t => t.ExamId == examId)
                .ToListAsync();
        }
    }
}
