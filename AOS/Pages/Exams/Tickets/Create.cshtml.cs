using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AOS.Data;
using AOS.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace AOS.Pages.Exams.Tickets
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Exam Exam { get; set; }

        public IActionResult OnGet(int? examId)
        {
            if (examId == null)
            {
                return NotFound("Не выбран экзамен");
            }

            Exam = _context.Exams.FirstOrDefault(p => p.Id == examId);

            if (Exam == null)
            {
                return NotFound("Не найден экзамен");
            }

            return Page();
        }

        [BindProperty]
        public TicketCreateViewModel UploadTicket { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var ticket = new Ticket()
            {
                Name = UploadTicket.Name,
                FileName = Path.GetFileNameWithoutExtension(UploadTicket.File.FileName),
                FileExtension = Path.GetExtension(UploadTicket.File.FileName),
                ContentType = UploadTicket.File.ContentType,
                ExamId = UploadTicket.ExamId
            };
            using (var reader = new BinaryReader(UploadTicket.File.OpenReadStream()))
            {
                ticket.TicketFile = new TicketFile
                {
                    Data = reader.ReadBytes((int)UploadTicket.File.Length)
                };
            }

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { examId = ticket.ExamId });
        }
    }
}
