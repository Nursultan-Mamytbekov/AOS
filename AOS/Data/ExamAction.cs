using System;
using System.ComponentModel.DataAnnotations;

namespace AOS.Data
{
    public class ExamAction
    {
        public int Id { get; set; }
        [Display(Name = "Дата начала")]
        public DateTime DateStart { get; set; }
        [Display(Name = "Дата окончания")]
        public DateTime DateEnd { get; set; }
        public int ExamId { get; set; }
        [Display(Name = "Дата экзамен")]
        public Exam Exam { get; set; }
        [Display(Name = "Статус")]
        [UIHint("_IsActive")]
        public bool IsActive { get; set; }
    }
}
