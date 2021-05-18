using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;

namespace AOS.Pages.Exams.UserTickets
{
    public class GetUserTicketModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GetUserTicketModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ExamUserTicket ExamUserTicket { get; set; }

        public async Task<IActionResult> OnGetAsync(int? actionId)
        {
            if (actionId == null)
            {
                return NotFound();
            }

            ExamUserTicket = await _context.UserTickets
                .Include(e => e.ExamAction)
                .Include(e => e.User).FirstOrDefaultAsync(m => m.Id == id);

            if (ExamUserTicket == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
