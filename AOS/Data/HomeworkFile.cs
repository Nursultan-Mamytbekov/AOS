using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Data
{
    public class HomeworkFile
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public Homework Homework { get; set; }
    }
}
