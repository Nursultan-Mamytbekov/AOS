using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOS.Data
{
    public class TicketFile
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public Ticket Ticket { get; set; }
    }
}
