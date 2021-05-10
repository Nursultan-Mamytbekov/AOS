using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Data
{
    public class Result
    {
        public int Id { get; set; }
        [Display(Name = "Оценка")]
        public int Rate { get; set; }
        [Display(Name = "Рецензия")]
        public string Review { get; set; }
        public int HomeworkId { get; set; }
        [Display(Name = "Работа")]
        public Homework Homework { get; set; }
    }
}
