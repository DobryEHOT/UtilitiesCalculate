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
    internal class ElectricPowerUtilities : UtilitiesUniversal<UtilitiesContext, ElectricPowerResult>, IStratageResult<ElectricPowerContext, ElectricPowerResult>
    {
        private decimal DayTariff;
        private decimal NightTariff;
        private decimal Normative;
        private decimal NormativeTariff;
        private decimal MaxMeter;
        public ElectricPowerUtilities()
        {
            var tariffNormatives = Singleton<TariffNormative>.MainSingleton.TariffNormativeData;
            if (tariffNormatives != null)
            {
                DayTariff = tariffNormatives.eeDayTariff;
                NightTariff = tariffNormatives.eeNightTariff;

                Normative = tariffNormatives.eeNormative;
                NormativeTariff = tariffNormatives.eeTariff;
            }
            MaxMeter = Singleton<Meters>.MainSingleton.MetersData?.ElecticPowerMeter ?? 0;
        }
        public ElectricPowerResult DoStratage(ElectricPowerContext context)
        {
            if (context.MeteringDevice)
            {
                return CalculateAllMetering(context);
            }
            else
            {
                var universalContext = new UtilitiesContext()
                {
                    Residents = context.Residents,
                    MeteringDevice = context.MeteringDevice
                };
                return base.CalucateAll(universalContext);
            }
        }
        private ElectricPowerResult CalculateAllMetering(ElectricPowerContext context)
        {
            var dayVolume = CalculateMeteringDeviceAlgorithm(new UtilitiesContext()
            {
                CurrentReadings = context.CurrentReadingsDay,
                PreviousReadings = context.PreviousReadingsDay
            });
            var nightVolume = CalculateMeteringDeviceAlgorithm(new UtilitiesContext()
            {
                CurrentReadings = context.CurrentReadingsNight,
                PreviousReadings = context.PreviousReadingsNight
            });
            var dayCost = dayVolume * DayTariff;
            var nightCost = nightVolume * NightTariff;

            return new ElectricPowerResult()
            {
                UtilitiesCostDay = dayCost,
                UtilitiesCostNight = nightCost,
                UtilitiesCost = dayCost + nightCost,
                VolumeDay = dayVolume,
                VolumeNight = nightVolume,
                Volume = dayVolume + nightVolume
            };
        }
        protected override decimal CalculateGeneralAlgorithm(ElectricPowerResult resultData)
        {
            return resultData.Volume * NormativeTariff;
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
            return context.Residents * Normative;
        }
    }

    internal class ElectricPowerContext : IUtilitiesContext
    {
        public bool MeteringDevice { get; set; }
        public decimal CurrentReadingsDay { get; set; }
        public decimal PreviousReadingsDay { get; set; }
        public decimal CurrentReadingsNight { get; set; }
        public decimal PreviousReadingsNight { get; set; }
        public int Residents { get; set; }
    }

    internal class ElectricPowerResult : IUtilitiesResult
    {
        public decimal UtilitiesCost { get; set; }
        public decimal Volume { get; set; }
        public decimal UtilitiesCostNight { get; set; }
        public decimal VolumeNight { get; set; }
        public decimal UtilitiesCostDay { get; set; }
        public decimal VolumeDay { get; set; }
    }
}
