using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskUSC.Data
{
    internal class MetersData
    {
        public decimal ColdWaterMeter { get; }
        public decimal HotWaterMeter { get; }
        public decimal ElecticPowerMeter { get; }
        public MetersData(decimal coldWaterMeter, decimal hotWaterMeter, decimal electicPowerMeter)
        {
            ColdWaterMeter = coldWaterMeter;
            HotWaterMeter = hotWaterMeter;
            ElecticPowerMeter = electicPowerMeter;
        }
    }
}
