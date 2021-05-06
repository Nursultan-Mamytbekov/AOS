using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;

namespace AOS.Pages.Events.Homeworks
{
    public class IndexModel : PageModel
    {
        private readonly AOS.Data.ApplicationDbContext _context;

        public IndexModel(AOS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Homework> Homework { get;set; }

        public async Task OnGetAsync(int? id)
        {
            Homework = await _context.Homeworks
                .Include(h => h.User)
                .Include(h => h.Material)
                .Where(h => h.MaterialId == id)
                .ToListAsync();
        }
    }
}
