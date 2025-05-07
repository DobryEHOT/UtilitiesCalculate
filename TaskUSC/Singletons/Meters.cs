using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskUSC.Architecture.GoF.Creational;
using TaskUSC.Data;

namespace TaskUSC.Singletons
{
    internal class Meters : Singleton<Meters>
    {
        public MetersData? MetersData { get; set; }
    }
}
