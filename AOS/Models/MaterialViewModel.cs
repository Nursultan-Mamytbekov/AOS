using Microsoft.AspNetCore.Http;

using System;
using System.ComponentModel.DataAnnotations;

namespace AOS.Models
{
    public class MaterialViewModel
    {
        [Display(Name = "Название файла")]
        public string Name { get; set; }
        [Display(Name = "Статус")]
        [UIHint("_BoolDropDownList")]
        public bool IsActive { get; set; }
        [Display(Name = "Файл")]
        public IFormFile File { get; set; }
        [Display(Name = "Предмет")]
        public int SubjectId { get; set; }
        [Display(Name = "Срок сдачи")]
        public DateTime DeadLine { get; set; }
    }
}
