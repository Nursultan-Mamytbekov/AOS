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
        [Display(Name = "Студент")]
        public User User { get; set; }
        public int TicketId { get; set; }
        public int ExamActionId { get; set; }
        public ExamAction ExamAction { get; set; }
    }
}
