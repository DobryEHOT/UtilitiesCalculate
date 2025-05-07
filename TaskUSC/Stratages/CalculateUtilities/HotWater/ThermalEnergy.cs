using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskUSC.Architecture.GoF.Behavioral;
using TaskUSC.Architecture.GoF.Creational;
using TaskUSC.Singletons;
using TaskUSC.Stratages.CalculateUtilities.Interfaces;

namespace TaskUSC.Stratages.CalculateUtilities.HotWater
{
    internal class ThermalEnergy : IStratageResult<ThermalEnergyUtilitiesContext, ThermalEnergyUtilitiesResult>
    {
        private decimal TeTariff;
        private decimal TeNormative;
        public ThermalEnergy()
        {
            var tariffNormatives = Singleton<TariffNormative>.MainSingleton.TariffNormativeData;
            if (tariffNormatives != null)
            {
                TeTariff = tariffNormatives.teTariff;
                TeNormative = tariffNormatives.teNormative;
            }
        }
        public ThermalEnergyUtilitiesResult DoStratage(ThermalEnergyUtilitiesContext context)
        {
            var volume = context.HeatCarrierVolume * TeNormative;
            var cost = volume * TeTariff;
            return new ThermalEnergyUtilitiesResult()
            {
                Volume = volume,
                UtilitiesCost = cost
            };
        }
    }

    internal class ThermalEnergyUtilitiesContext : IUtilitiesContext
    {
        public bool MeteringDevice { get; set; }
        public int Residents { get; set; }
        public decimal HeatCarrierVolume { get; set; }
    }

    internal class ThermalEnergyUtilitiesResult : IUtilitiesResult
    {
        public decimal UtilitiesCost { get; set; }
        public decimal Volume { get; set; }
    }
}
