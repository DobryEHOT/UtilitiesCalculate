using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskUSC.Models
{
    internal class HeatCarrier
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public decimal Volume { get; set; }
        public decimal MeteringDeviceReadings { get; set; }
    }
}
