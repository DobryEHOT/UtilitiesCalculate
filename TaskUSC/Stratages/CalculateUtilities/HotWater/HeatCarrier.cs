using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskUSC.Architecture.GoF.Behavioral;
using TaskUSC.Architecture.GoF.Creational;
using TaskUSC.Singletons;

namespace TaskUSC.Stratages.CalculateUtilities.HotWater
{
    internal class HeatCarrier : UtilitiesUniversal<UtilitiesContext, UtilitiesResult>, IStratageResult<UtilitiesContext, UtilitiesResult>
    {
        private decimal TnTariff;
        private decimal TnNormative;
        private decimal MaxMeter;

        public HeatCarrier()
        {
            var tariffNormatives = Singleton<TariffNormative>.MainSingleton.TariffNormativeData;
            if (tariffNormatives != null)
            {
                TnTariff = tariffNormatives.tnTariff;
                TnNormative = tariffNormatives.tnNormative;
            }
            MaxMeter = Singleton<Meters>.MainSingleton.MetersData?.HotWaterMeter ?? 0;
        }

        public UtilitiesResult DoStratage(UtilitiesContext context)
        {
            return CalucateAll(context);
        }

        protected override decimal CalculateGeneralAlgorithm(UtilitiesResult volume)
        {
            return volume.Volume * TnTariff;
        }

        protected override decimal CalculateMeteringDeviceAlgorithm(UtilitiesContext context)
        {
            var previous = context.PreviousReadings;
            var current = context.CurrentReadings;

            if (current >= previous)
                return current - previous;

            return current + (MaxMeter - previous);
        }

        protected override decimal CalculateNormativeAlgorithm(UtilitiesContext context)
        {
            return context.Residents * TnNormative;
        }
    }
}
