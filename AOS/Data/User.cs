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
        public ICollection<Material> Materials { get; set; }
    }
}
