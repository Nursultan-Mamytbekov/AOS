using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AOS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AOS.Pages.Results.Exams
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
        public ExamResultGrade ExamResultGrade { get; set; }
        public ExamResult ExamResult { get; set; }

        public IActionResult OnGet(int? examResultId)
        {
            if (examResultId == null)
            {
                return NotFound("Не выбрана работа");
            }

            ExamResult = _context.ExamResults.Include(p => p.ExamUserTicket).FirstOrDefault(p => p.Id == examResultId);

            if (ExamResult == null)
            {
                return NotFound("Не найдена работа");
            }
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? actionId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _context.ExamResultGrades.FirstOrDefaultAsync(p => p.ExamResultId == ExamResultGrade.ExamResultId);

            if (result != null)
            {
                result.Rate = ExamResultGrade.Rate;
                result.Review = ExamResultGrade.Review;
            }
            else
            {
                ExamResultGrade.Teacher = await GetCurrentUser();
                _context.ExamResultGrades.Add(ExamResultGrade);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Exams/ExamResults/Index", new { actionId = actionId});
        }

        private async Task<User> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }
    }
}
