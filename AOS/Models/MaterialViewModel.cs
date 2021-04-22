using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Models
{
    public class MaterialViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public IFormFile File { get; set; }
        public int SubjectId { get; set; }
    }
}
