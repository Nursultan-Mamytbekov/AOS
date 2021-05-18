using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Models
{
    public class TicketCreateViewModel
    {
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Файл")]
        public IFormFile File { get; set; }
        public int ExamId { get; set; }
    }
}
