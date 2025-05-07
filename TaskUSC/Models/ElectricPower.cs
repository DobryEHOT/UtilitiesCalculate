using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskUSC.Models
{
    internal class ElectricPower
    {
        public int Id { get; set; }
        public decimal CostDay { get; set; }
        public decimal VolumeDay { get; set; }
        public decimal CostNight { get; set; }
        public decimal VolumeNight { get; set; }
        public decimal MeteringDeviceReadingsDay { get; set; }
        public decimal MeteringDeviceReadingsNight { get; set; }
    }
}
