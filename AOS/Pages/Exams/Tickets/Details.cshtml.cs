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
    public class DetailsModel : PageModel
    {
        private readonly AOS.Data.ApplicationDbContext _context;

        public DetailsModel(AOS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Ticket Ticket { get; set; }

        [BindProperty]
        public int? Id { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket = await _context.Tickets
                .Include(t => t.Exam).FirstOrDefaultAsync(m => m.Id == id);

            if (Ticket == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Id == null) return NotFound();
            var file = _context.Tickets.Include(p => p.TicketFile).FirstOrDefault(p => p.Id == Id);
            if (file == null) return NotFound();
            return File(file.TicketFile.Data, file.ContentType, file.FileName + file.FileExtension);
        }
    }
}
