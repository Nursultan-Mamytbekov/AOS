﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Data
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] File { get; set; }
        public bool IsActive { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
