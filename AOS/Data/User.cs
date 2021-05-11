using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Data
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Material> Materials { get; set; }
        public ICollection<Homework> Homeworks { get; set; }
        public ICollection<Result> Results { get; set; }
    }
}
