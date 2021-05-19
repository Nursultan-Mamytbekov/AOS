using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Data
{
    public class ExamResultGrade
    {
        public int Id { get; set; }
        [Display(Name = "Оценка")]
        public int Rate { get; set; }
        [Display(Name = "Рецензия")]
        public string Review { get; set; }
        public int ExamResultId { get; set; }
        [Display(Name = "Экзаменационная работа")]
        public ExamResult ExamResult { get; set; }
        public string TeacherId { get; set; }
        [Display(Name = "Преподаватель")]
        public User Teacher { get; set; }
    }
}
