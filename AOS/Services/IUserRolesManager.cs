using System.Security.Claims;
using System.Threading.Tasks;

namespace AOS.Services
{
    public interface IUserRolesManager
    {
        public Task<bool> IsTeacher(ClaimsPrincipal username);
        public Task<bool> IsStudent(ClaimsPrincipal username);
    }
}
