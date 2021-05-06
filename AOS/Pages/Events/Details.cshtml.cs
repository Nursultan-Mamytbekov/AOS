using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AOS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using AOS.Models;
using System.IO;

namespace AOS.Pages.Events
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsModel(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public Material Material { get; set; }

        public Homework Homework { get; set; }

        [BindProperty]
        public int? Id { get; set; }

        [BindProperty]
        public HomeworkCreateViewModel UploadHomeworkModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Material = await _context.Materials
                .Include(m => m.Subject).FirstOrDefaultAsync(m => m.Id == id);

            var user = await GetCurrentUser();
            Homework = await _context.Homeworks.FirstOrDefaultAsync(p => p.User == user);            

            if (Material == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Id == null) return NotFound();
            var file = _context.Materials.Include(p => p.MaterialFile).FirstOrDefault(p => p.Id == Id);
            if (file == null) return NotFound();
            return File(file.MaterialFile.Data, file.ContentType, file.FileName + file.FileExtension);
        }

        public async Task<IActionResult> OnPostUploadHomeworkAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await GetCurrentUser();
            var homework = await _context.Homeworks.Include(p => p.HomeworkFile).FirstOrDefaultAsync(p => p.User == user);

            if (homework != null)
            {
                homework.FileName = Path.GetFileNameWithoutExtension(UploadHomeworkModel.File.FileName);
                homework.FileExtension = Path.GetExtension(UploadHomeworkModel.File.FileName);
                homework.ContentType = UploadHomeworkModel.File.ContentType;
                homework.MaterialId = UploadHomeworkModel.MaterialId;
                homework.MaterialId = UploadHomeworkModel.MaterialId;
                using (var reader = new BinaryReader(UploadHomeworkModel.File.OpenReadStream()))
                {
                    homework.HomeworkFile.Data = reader.ReadBytes((int)UploadHomeworkModel.File.Length);
                }
            }
            else
            {
                var newHomework = new Homework()
                {
                    FileName = Path.GetFileNameWithoutExtension(UploadHomeworkModel.File.FileName),
                    FileExtension = Path.GetExtension(UploadHomeworkModel.File.FileName),                
                    ContentType = UploadHomeworkModel.File.ContentType,
                    MaterialId = UploadHomeworkModel.MaterialId,
                    User = await GetCurrentUser()
                };
                using (var reader = new BinaryReader(UploadHomeworkModel.File.OpenReadStream()))
                {
                    newHomework.HomeworkFile = new HomeworkFile
                    {
                        Data = reader.ReadBytes((int)UploadHomeworkModel.File.Length)
                    };
                }
                _context.Homeworks.Add(newHomework);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = UploadHomeworkModel.MaterialId });
        }

        private async Task<User> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }

    }
}
