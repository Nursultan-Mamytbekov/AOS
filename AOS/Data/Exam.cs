using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AOS.Data
{
    public class Exam
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Exam> Exams { get; set; }
    }
}
