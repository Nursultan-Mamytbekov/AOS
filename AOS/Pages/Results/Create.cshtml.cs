using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AOS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AOS.Pages.Results
{
    [Authorize(Roles = "teacher")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Result Result { get; set; }
        public Homework Homework { get; set; }

        public IActionResult OnGet(int? homeworkId)
        {
            if (homeworkId == null)
            {
                return NotFound("Не выбрана домашняя работа");
            }

            Homework = _context.Homeworks.Include(p => p.Material).FirstOrDefault(p => p.Id == homeworkId);

            if (Homework == null)
            {
                return NotFound("Не найдена домашняя работа");
            }
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _context.Results.FirstOrDefaultAsync(p => p.HomeworkId == Result.HomeworkId);

            if (result != null)
            {
                result.Rate = Result.Rate;
                result.Review = Result.Review;
            }
            else
            {
                Result.Teacher = await GetCurrentUser();
                _context.Results.Add(Result);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Events/Homeworks/Details", new { id = Result.HomeworkId });
        }

        private async Task<User> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }
    }
}
