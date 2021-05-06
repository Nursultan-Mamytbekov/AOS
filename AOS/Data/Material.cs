using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AOS.Data
{
    public class Material
    {
        public int Id { get; set; }
        [Display(Name = "Дата загрузки")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Display(Name = "Название файла")]
        public string FileName { get; set; }
        [Display(Name = "Тип файла")]
        public string FileExtension { get; set; }
        public string ContentType { get; set; }
        [Display(Name = "Статус")]
        [UIHint("_IsActive")]
        public bool IsActive { get; set; }
        [Display(Name = "Срок сдачи")]
        public DateTime DeadLine { get; set; }
        [Display(Name = "Предмет")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int MaterialFileId { get; set; }
        public MaterialFile MaterialFile { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Homework> Homeworks { get; set; }
    }
}
