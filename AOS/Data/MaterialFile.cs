using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Data
{
    public class MaterialFile
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public Material Material { get; set; }
    }
}
