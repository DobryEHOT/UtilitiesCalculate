using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskUSC.Facades;

namespace TaskUSC.Models
{
    internal class UtilitiesData
    {
        public int Id { get; set; }
        public int CountResidents { get; set; }
        public ColdWater? СoldWater { get; set; }
        public HeatCarrier? HeatCarrier { get; set; }
        public ThermalEnergy? ThermalEnergy { get; set; }
        public ElectricPower? ElectricPower { get; set; }

        public static UtilitiesData CreateModel(CalculteUtilitiesResult utilitiesOutput, CalculteUtilitiesContext utilitiesInput)
        {
            if (utilitiesOutput == null)
                throw new ArgumentNullException(nameof(utilitiesOutput));

            return new UtilitiesData
            {
                Id = 0,

                CountResidents = utilitiesInput.ColdWaterContext.Residents,

                СoldWater = new ColdWater
                {
                    Id = 0,
                    Cost = utilitiesOutput.ColdWaterResult?.UtilitiesCost ?? 0,
                    Volume = utilitiesOutput.ColdWaterResult?.Volume ?? 0,
                    MeteringDeviceReadings = utilitiesInput.ColdWaterContext?.CurrentReadings ?? 0
                },

                HeatCarrier = new HeatCarrier
                {
                    Id = 0,
                    Cost = utilitiesOutput.HotWaterResult?.HeatCarrierResult?.UtilitiesCost ?? 0,
                    Volume = utilitiesOutput.HotWaterResult?.HeatCarrierResult?.Volume ?? 0,
                    MeteringDeviceReadings = utilitiesInput.HotWaterContext?.CurrentReadings ?? 0
                },

                ThermalEnergy = new ThermalEnergy
                {
                    Id = 0,
                    Cost = utilitiesOutput.HotWaterResult?.ThermalEnergyUtilitiesResult?.UtilitiesCost ?? 0,
                    Volume = utilitiesOutput.HotWaterResult?.ThermalEnergyUtilitiesResult?.Volume ?? 0
                },

                ElectricPower = new ElectricPower
                {
                    Id = 0,
                    CostDay = utilitiesOutput.ElectricPowerResult?.UtilitiesCostDay ?? 0,
                    VolumeDay = utilitiesOutput.ElectricPowerResult?.VolumeDay ?? 0,
                    CostNight = utilitiesOutput.ElectricPowerResult?.UtilitiesCostNight ?? 0,
                    VolumeNight = utilitiesOutput.ElectricPowerResult?.VolumeNight ?? 0,
                    MeteringDeviceReadingsDay = utilitiesInput.ElectricPowerContext?.CurrentReadingsDay ?? 0,
                    MeteringDeviceReadingsNight = utilitiesInput.ElectricPowerContext?.CurrentReadingsNight ?? 0
                }
            };
        }
    }
}
