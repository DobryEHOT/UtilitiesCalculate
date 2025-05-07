using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskUSC.Stratages.CalculateUtilities.Interfaces
{
    internal interface IUtilitiesContext
    {
        public bool MeteringDevice { get; set; }
        public int Residents { get; set; }
    }

    internal interface IUtilitiesResult
    {
        public decimal UtilitiesCost { get; set; }
        public decimal Volume { get; set; }
    }
}
