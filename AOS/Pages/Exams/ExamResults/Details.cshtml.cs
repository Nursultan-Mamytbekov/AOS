using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;

namespace AOS.Pages.Exams.ExamResults
{
    public class DetailsModel : PageModel
    {
        private readonly AOS.Data.ApplicationDbContext _context;

        public DetailsModel(AOS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ExamResult ExamResult { get; set; }
        public Ticket Ticket { get; set; }


        [BindProperty]
        public int? Id { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExamResult = await _context.ExamResults
                .Include(e => e.ExamUserTicket)
                .Include(e => e.ExamUserTicket.User)
                .Include(e => e.ExamUserTicket.ExamAction)
                .FirstOrDefaultAsync(m => m.Id == id);

            Ticket = await _context.Tickets.FirstOrDefaultAsync(p => p.ExamId == ExamResult.ExamUserTicket.ExamAction.ExamId);

            if (ExamResult == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Id == null) return NotFound();
            var file = _context.ExamResults.Include(p => p.ExamResultFile).FirstOrDefault(p => p.Id == Id);
            if (file == null) return NotFound();
            return File(file.ExamResultFile.Data, file.ContentType, file.FileName + file.FileExtension);
        }
    }
}
