using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Data
{
    public class Homework
    {
        public int Id { get; set; }
        [Display(Name = "Дата загрузки")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Display(Name = "Название файла")]
        public string FileName { get; set; }
        [Display(Name = "Тип файла")]
        public string FileExtension { get; set; }
        public string ContentType { get; set; }
        [Display(Name = "Задание")]
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public int HomeworkFileId { get; set; }
        public HomeworkFile HomeworkFile { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Студент")]
        public User User { get; set; }
    }
}
