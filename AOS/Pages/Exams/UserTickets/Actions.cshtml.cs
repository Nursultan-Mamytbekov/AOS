using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AOS.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AOS.Pages.Exams.UserTickets
{
    public class ActionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public ActionsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ExamAction> Actions { get; set; }
        [BindProperty]
        public int? ExamId { get; set; }
        [BindProperty]
        public int? ActionId { get; set; }
        public void OnGet()
        {
            Actions = _context.ExamActions.Include(p => p.Exam).Where(p => p.IsActive).ToList();
        }

        public IActionResult OnPost()
        {
            var tickets = _context.Tickets.Include(p => p.Exam).Where(p => p.Exam.Id == ExamId).ToList();
            var action = _context.ExamActions.FirstOrDefault(p => p.Id == ActionId);

            if (tickets.Count == 0)
            {
                return NotFound("Нет билетов");
            }
            Random rand = new Random();
            int toSkip = rand.Next(1, tickets.Count);

            var ticket = tickets.Skip(toSkip).Take(1).First();

            return RedirectToPage("./RandTicket", new { ticketId = ticket.Id, examActionId = action.Id });
        }
    }
}
