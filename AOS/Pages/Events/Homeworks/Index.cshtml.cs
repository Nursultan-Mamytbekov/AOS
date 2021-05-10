using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;
using Microsoft.AspNetCore.Authorization;

namespace AOS.Pages.Events.Homeworks
{
    [Authorize(Roles = "teacher")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Homework> Homeworks { get;set; }

        public Material Material { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Material = await _context.Materials.FirstOrDefaultAsync(p => p.Id == id);          

            Homeworks = await _context.Homeworks
                .Include(h => h.User)
                .Include(h => h.Material)
                .Where(h => h.MaterialId == id)
                .ToListAsync();
        }
    }
}
