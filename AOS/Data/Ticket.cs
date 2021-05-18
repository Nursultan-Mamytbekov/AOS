using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AOS.Data
{
    public class Ticket
    {
        public int Id { get; set; }
        [Display(Name = "Название билета")]
        public string Name { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string ContentType { get; set; }
        public int ExamId { get; set; }
        [Display(Name = "Экзамен")]
        public Exam Exam { get; set; }
        public int TicketFileId { get; set; }
        public TicketFile TicketFile { get; set; }
    }
}
