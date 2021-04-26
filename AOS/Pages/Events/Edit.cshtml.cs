using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AOS.Data;
using AOS.Models;
using Microsoft.AspNetCore.Authorization;

namespace AOS.Pages.Events
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MaterialEditViewModel EditViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditViewModel = await _context.Materials
                .Include(m => m.Subject)
                .Include(m => m.File)
                .Select(m => new MaterialEditViewModel
                {
                    Id = m.Id,
                    FileName = m.FileName,
                    IsActive = m.IsActive,
                    SubjectId = m.SubjectId,
                })
                .FirstOrDefaultAsync(m => m.Id == id);            

            if (EditViewModel == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Material material = await _context.Materials
                .Include(m => m.Subject)
                .Include(m => m.File)
                .FirstOrDefaultAsync(material => material.Id == EditViewModel.Id);

            try
            {
                material.FileName = EditViewModel.FileName;
                material.IsActive = EditViewModel.IsActive;
                material.SubjectId = EditViewModel.SubjectId;
                _context.Update(material);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(material.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MaterialExists(int id)
        {
            return _context.Materials.Any(e => e.Id == id);
        }
    }
}
