using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskUSC.Architecture.GoF.Behavioral;
using TaskUSC.Architecture.GoF.Creational;
using TaskUSC.Singletons;
using TaskUSC.Stratages.CalculateUtilities.Interfaces;

namespace TaskUSC.Stratages.CalculateUtilities
{
    internal class ColdWaterUtilities : UtilitiesUniversal<UtilitiesContext, UtilitiesResult>, IStratageResult<UtilitiesContext, UtilitiesResult>
    {
        private decimal Tariff;
        private decimal Normative;
        private decimal MaxMeter;
        public ColdWaterUtilities()
        {
            var tariffNormatives = Singleton<TariffNormative>.MainSingleton.TariffNormativeData;
            if (tariffNormatives != null)
            {
                Tariff = tariffNormatives.cTariff;
                Normative = tariffNormatives.cNormative;
            }
            MaxMeter = Singleton<Meters>.MainSingleton.MetersData?.ColdWaterMeter ?? 0;
        }
        public UtilitiesResult DoStratage(UtilitiesContext context) => base.CalucateAll(context);
        protected override decimal CalculateGeneralAlgorithm(UtilitiesResult resultData) => resultData.Volume * Tariff;
        protected override decimal CalculateNormativeAlgorithm(UtilitiesContext context) => context.Residents * Normative;
        protected override decimal CalculateMeteringDeviceAlgorithm(UtilitiesContext context)
        {
            var previous = context.PreviousReadings;
            var current = context.CurrentReadings;

            if (current >= previous)
                return current - previous;

            return current + (MaxMeter - previous);
        }
    }

    internal class UtilitiesContext : IUtilitiesContext
    {
        public bool MeteringDevice { get; set; }
        public decimal CurrentReadings { get; set; }
        public decimal PreviousReadings { get; set; }
        public int Residents { get; set; }
    }

    internal class UtilitiesResult : IUtilitiesResult
    {
        public decimal UtilitiesCost { get; set; }
        public decimal Volume { get; set; }
    }
}
