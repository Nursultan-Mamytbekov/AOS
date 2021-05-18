using System;
using System.ComponentModel.DataAnnotations;

namespace AOS.Data
{
    public class ExamUserTicket
    {
        public int Id { get; set; }
        [Display(Name = "Дата получения билета")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        [Display(Name = "Дата студент")]
        public User User { get; set; }
        public int ExamActionId { get; set; }
        [Display(Name = "Событие экзамена")]
        public ExamAction ExamAction { get; set; }
    }
}
