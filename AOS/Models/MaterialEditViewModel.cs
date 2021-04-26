using System;
using System.ComponentModel.DataAnnotations;

namespace AOS.Models
{
    public class MaterialEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название файла")]
        public string FileName { get; set; }
        [Display(Name = "Статус")]
        [UIHint("_BoolDropDownList")]
        public bool IsActive { get; set; }
        [Display(Name = "Предмет")]
        public int SubjectId { get; set; }
    }
}
