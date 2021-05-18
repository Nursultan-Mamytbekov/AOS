using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace AOS.Pages.Exams.UserTickets
{
    public class RandTicketModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RandTicketModel(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public Ticket Ticket { get; set; }
        [BindProperty]
        public int? Id { get; set; }

        public async Task<IActionResult> OnGetAsync(int? ticketId, int? actionId)
        {
            if (ticketId == null)
            {
                return NotFound();
            }

            Ticket = await _context.Tickets
                .Include(t => t.Exam)
                .FirstOrDefaultAsync(m => m.Id == ticketId);

            var action = await _context.ExamActions.FirstOrDefaultAsync(p => p.Id == actionId);

            if (Ticket == null)
            {
                return NotFound();
            }

            var userTicket = new ExamUserTicket
            {
                User = await GetCurrentUser(),
                ExamAction = action
            };

            _context.UserTickets.Add(userTicket);
            await _context.SaveChangesAsync();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (Id == null) return NotFound();
            var file = _context.Tickets.Include(p => p.TicketFile).FirstOrDefault(p => p.Id == Id);
            if (file == null) return NotFound();
            return File(file.TicketFile.Data, file.ContentType, file.FileName + file.FileExtension);
        }

        private async Task<User> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }
    }
}
