using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskUSC.Architecture.GoF.Behavioral;
using TaskUSC.Stratages.CalculateUtilities.HotWater;
using TaskUSC.Stratages.CalculateUtilities.Interfaces;

namespace TaskUSC.Stratages.CalculateUtilities
{
    internal class HotWaterUtilities :  IStratageResult<UtilitiesContext, HotWaterUtilitiesResult>
    {
        private IStratageResult<UtilitiesContext, UtilitiesResult> heatCarrier = new HeatCarrier();
        private IStratageResult<ThermalEnergyUtilitiesContext, ThermalEnergyUtilitiesResult> thermalEnergy = new ThermalEnergy();

        public HotWaterUtilitiesResult DoStratage(UtilitiesContext context)
        {
            var hcResult = heatCarrier.DoStratage(context);
            var hcContext = new ThermalEnergyUtilitiesContext()
            {
                HeatCarrierVolume = hcResult.Volume
            };

            var teResult = thermalEnergy.DoStratage(hcContext);
            return new HotWaterUtilitiesResult(hcResult, teResult);
        }
    }

    internal class HotWaterUtilitiesResult
    {
        public decimal UtilitiesCost { get; set; }
        public UtilitiesResult HeatCarrierResult { get; private set; }
        public ThermalEnergyUtilitiesResult ThermalEnergyUtilitiesResult { get; private set; }
        public HotWaterUtilitiesResult(UtilitiesResult heatCarrierResult, ThermalEnergyUtilitiesResult thermalEnergyUtilitiesResult)
        {
            HeatCarrierResult = heatCarrierResult;
            ThermalEnergyUtilitiesResult = thermalEnergyUtilitiesResult;
            UtilitiesCost = heatCarrierResult.UtilitiesCost + thermalEnergyUtilitiesResult.UtilitiesCost;
        }
    }
}
