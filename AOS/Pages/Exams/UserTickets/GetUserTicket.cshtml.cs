using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AOS.Pages.Exams.UserTickets
{
    [Authorize(Roles = "student")]
    public class GetUserTicketModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserTicketModel(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public ExamAction Action { get; set; }
        public Ticket Ticket { get; set; }
        public ExamUserTicket ExamUserTicket { get; set; }
        public ExamResult ExamResult { get; set; }

        [BindProperty]
        [Display(Name = "Файл")]
        public IFormFile UploadExamFile { get; set; }
        [BindProperty]
        public int? ExamId { get; set; }
        [BindProperty]
        public int? ActionId { get; set; }
        [BindProperty]
        public int? UserTicketId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? actionId)
        {
            if (actionId == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUser();
            Action = await _context.ExamActions.Include(m => m.Exam).FirstOrDefaultAsync(m => m.Id == actionId);
            ExamUserTicket = await _context.UserTickets.FirstOrDefaultAsync(p => p.ExamActionId == Action.Id && p.User == user);

            if (ExamUserTicket != null)
            {
                Ticket = await _context.Tickets.FirstOrDefaultAsync(p => p.Id == ExamUserTicket.TicketId);
                ExamResult = await _context.ExamResults.FirstOrDefaultAsync(p => p.ExamUserTicketId == ExamUserTicket.Id);
            }

            if (Action == null)
            {
                return NotFound();
            }


            return Page();
        }

        public IActionResult OnPost()
        {
            var tickets = _context.Tickets.Include(p => p.Exam).Where(p => p.Exam.Id == ExamId).ToList();
            var action = _context.ExamActions.FirstOrDefault(p => p.Id == ActionId);

            if (tickets.Count() == 0)
            {
                return NotFound("Нет билетов");
            }

            Random rand = new Random();
            int toSkip = rand.Next(0, tickets.Count);

            var ticket = tickets.Skip(toSkip).Take(1).First();

            return RedirectToPage("./RandTicket", new { ticketId = ticket.Id, actionId = action.Id });
        }

        public async Task<IActionResult> OnPostUploadExam(int? actionId)
        {
            if (!ModelState.IsValid || actionId == null) return NotFound();

            var examResult = new ExamResult
            {
                ExamUserTicketId = Convert.ToInt32(UserTicketId),
                FileName = Path.GetFileNameWithoutExtension(UploadExamFile.FileName),
                FileExtension = Path.GetExtension(UploadExamFile.FileName),
                ContentType = UploadExamFile.ContentType
            };
            using (var reader = new BinaryReader(UploadExamFile.OpenReadStream()))
            {
                examResult.ExamResultFile = new ExamResultFile
                {
                    Data = reader.ReadBytes((int)UploadExamFile.Length)
                };
            }

            _context.ExamResults.Add(examResult);
            await _context.SaveChangesAsync();

            return RedirectToPage("./GetUserTicket", new { actionId = actionId });
        }
        private async Task<User> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }
    }
}
