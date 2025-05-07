using TaskUSC.Architecture.GoF.Creational;
using TaskUSC.Data;

namespace TaskUSC.Singletons
{
    internal class TariffNormative : Singleton<TariffNormative>
    {
        public TariffNormativeData? TariffNormativeData { get; set; }
    }
}
