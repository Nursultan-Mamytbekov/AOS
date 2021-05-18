﻿ using System;
using System.ComponentModel.DataAnnotations;

namespace AOS.Data
{
    public class ExamResult
    {
        public int Id { get; set; }
        [Display(Name = "Дата сдачи")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int ExamUserTicketId { get; set; }
        [Display(Name = "Билет студента")]
        public ExamUserTicket ExamUserTicket { get; set; }
        public int ExamResultFileId { get; set; }
        public ExamResultFile ExamResultFile { get; set; }
        [Display(Name = "Название файла")]
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string ContentType { get; set; }
    }
}
