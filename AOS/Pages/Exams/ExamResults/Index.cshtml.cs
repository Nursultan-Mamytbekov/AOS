using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace AOS.Pages.Exams.ExamResults
{
    [Authorize(Roles = "teacher")]
    public class IndexModel : PageModel
    {
        private readonly AOS.Data.ApplicationDbContext _context;

        public IndexModel(AOS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ExamResult> ExamResult { get;set; }
        public IList<Ticket> Tickets { get;set; }

        [BindProperty]
        public int? Id { get; set; }

        public async Task OnGetAsync(int? actionId)
        {
            ExamResult = await _context.ExamResults
                .Include(e => e.ExamUserTicket)
                .Include(e => e.ExamUserTicket.User)
                .Include(e => e.ExamUserTicket.ExamAction)
                .Where(e => e.ExamUserTicket.ExamActionId == actionId)
                .ToListAsync();

            var examId = ExamResult.Select(p => p.ExamUserTicket.ExamAction.ExamId).First();

            Tickets = await _context.Tickets.Where(p => p.ExamId == examId).ToListAsync();
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
