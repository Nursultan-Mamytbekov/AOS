using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AOS.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AOS.Pages.Exams.UserTickets
{
    [Authorize(Roles = "student")]
    public class ActionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public ActionsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ExamAction> Actions { get; set; }
        
        public void OnGet()
        {
            Actions = _context.ExamActions.Include(p => p.Exam).Where(p => p.IsActive).ToList();
        }        
    }
}
