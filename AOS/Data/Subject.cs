using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Data
{
    public class Subject
    {
        public int Id { get; set; }
        [Display(Name = "Название предмета")]
        public string Name { get; set; }
        public ICollection<Material> Materials { get; set; }
    }
}
