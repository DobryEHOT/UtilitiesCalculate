using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskUSC.Stratages.CalculateUtilities.Interfaces;

namespace TaskUSC.Stratages.CalculateUtilities
{
    internal abstract class UtilitiesUniversal<T, R>
        where T : IUtilitiesContext
        where R : IUtilitiesResult, new()
    {
        protected abstract decimal CalculateGeneralAlgorithm(R resultData);
        protected abstract decimal CalculateNormativeAlgorithm(T context);
        protected abstract decimal CalculateMeteringDeviceAlgorithm(T context); 
        protected virtual R CalucateAll(T context)
        {
            R result = new R();
            decimal volume;
            if (context.MeteringDevice)
                volume = CalculateMeteringDeviceAlgorithm(context);
            else
                volume = CalculateNormativeAlgorithm(context);

            result!.Volume = volume;
            result.UtilitiesCost = CalculateGeneralAlgorithm(result);
            return result;
        }
    }
}
