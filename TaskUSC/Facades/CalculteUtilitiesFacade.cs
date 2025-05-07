using TaskUSC.Architecture.GoF.Behavioral;
using TaskUSC.Stratages.CalculateUtilities;

namespace TaskUSC.Facades
{
    internal class CalculteUtilitiesFacade
    {
        private IStratageResult<UtilitiesContext, UtilitiesResult> coldWater = new ColdWaterUtilities();
        private IStratageResult<UtilitiesContext, HotWaterUtilitiesResult> hotWater = new HotWaterUtilities();
        private IStratageResult<ElectricPowerContext, ElectricPowerResult> electricPower = new ElectricPowerUtilities();
        private CalculteUtilitiesContext context;
        public CalculteUtilitiesFacade(CalculteUtilitiesContext context)
        {
            this.context = context;
        }

        public CalculteUtilitiesResult Calculate()
        {
            var coldWaterResult = coldWater.DoStratage(context.ColdWaterContext);
            var hotWaterResult = hotWater.DoStratage(context.HotWaterContext);
            var electricPowerResult = electricPower.DoStratage(context.ElectricPowerContext);

            var resultCalculate = new CalculteUtilitiesResult(coldWaterResult, hotWaterResult, electricPowerResult);
            return resultCalculate;
        }
    }

    internal class CalculteUtilitiesContext
    {
        public UtilitiesContext ColdWaterContext { get; set; }
        public UtilitiesContext HotWaterContext { get; set; }
        public ElectricPowerContext ElectricPowerContext { get; set; }
        public CalculteUtilitiesContext(UtilitiesContext coldWaterContext, UtilitiesContext hotWaterContext, ElectricPowerContext electricPowerContext)
        {
            ColdWaterContext = coldWaterContext;
            HotWaterContext = hotWaterContext;
            ElectricPowerContext = electricPowerContext;
        }
    }

    internal class CalculteUtilitiesResult
    {
        public UtilitiesResult ColdWaterResult { get; set; }
        public HotWaterUtilitiesResult HotWaterResult { get; set; }
        public ElectricPowerResult ElectricPowerResult { get; set; }
        public CalculteUtilitiesResult(UtilitiesResult coldWaterResult, HotWaterUtilitiesResult hotWaterResult, ElectricPowerResult electricPowerResult)
        {
            ColdWaterResult = coldWaterResult;
            HotWaterResult = hotWaterResult;
            ElectricPowerResult = electricPowerResult;
        }
    }
}
