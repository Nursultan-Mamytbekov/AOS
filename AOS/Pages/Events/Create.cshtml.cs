﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AOS.Data;
using AOS.Models;
using System.IO;

namespace AOS.Pages.Events
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public MaterialViewModel Material { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var material = new Material()
            {
                Name = Material.Name,
                IsActive = Material.IsActive,
                SubjectId = Material.SubjectId
            };
            using (var reader = new BinaryReader(Material.File.OpenReadStream()))
            {
                material.File = reader.ReadBytes((int)Material.File.Length);
            }
            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
