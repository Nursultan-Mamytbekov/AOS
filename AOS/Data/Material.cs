using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Data
{
    public class Material
    {
        public int Id { get; set; }
        [Display(Name = "Название файла")]
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte[] File { get; set; }
        [Display(Name = "Статус")]
        [UIHint("_IsActive")]
        public bool IsActive { get; set; }
        [Display(Name = "Предмет")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string ContentType { get; set; }
    }
}
