using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskUSC.Architecture.GoF.Behavioral;
using TaskUSC.Architecture.GoF.Creational;
using TaskUSC.DatabaseContexts;
using TaskUSC.Debug;
using TaskUSC.Facades;
using TaskUSC.Models;
using TaskUSC.Stratages.CalculateUtilities;

namespace TaskUSC.States.Utilities
{
    internal class UtilitiesStateMachine : StateMachine<UtilitiesStateMachine>
    {
        public CalculteUtilitiesContext? UtilitiesContext { get; set; }
        public CalculteUtilitiesResult? UtilitiesResult { get; set; }
        protected override void InicializeMachine()
        {
            AddState(new InputDataUtilitiesState());
            AddState(new CalculateUtilitiesState());
            AddState(new OutputDataUtilitiesState());
            AddState(new SaveDataUtilitiesState());
            AddState(new ResetDataBaseUtilitiesState());

            SwitchTo<InputDataUtilitiesState>();
        }
    }

    internal abstract class UtilitiesState : State<UtilitiesStateMachine>
    {
        public override void OnEnter(UtilitiesStateMachine machine)
        {
            base.OnEnter(machine);
            Singleton<Debuger>.MainSingleton.WriteLine(machine.GetType().Name + " Enter to state " + this.GetType().Name);
        }
        protected bool AskYesNo(string question)
        {
            Console.Write($"{question} (y/n): ");
            return Console.ReadLine().Trim().ToLower().Equals("y");
        }

        protected decimal GetDecimal()
        {
            decimal result;
            while (!decimal.TryParse(Console.ReadLine(), out result))
                Console.WriteLine("Вам необходимо ввести число:");

            return result;
        }

        protected int GetInt()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
                Console.WriteLine("Вам необходимо ввести целое число:");

            return result;
        }
    }

    internal class InputDataUtilitiesState : UtilitiesState
    {
        public override void OnEnter(UtilitiesStateMachine machine)
        {
            base.OnEnter(machine);

            int residents = TakeResidents();
            UtilitiesContext coldWaterContext = TakeColdWaterContext(residents);
            UtilitiesContext hotWaterContext = TakeHotWaterContext(residents);
            ElectricPowerContext electricPowerContext = TakeEPowerContext(residents);

            LoadLastData(coldWaterContext, hotWaterContext, electricPowerContext);

            var utilitiesContext = new CalculteUtilitiesContext(coldWaterContext, hotWaterContext, electricPowerContext);
            machine.UtilitiesContext = utilitiesContext;

            machine.SwitchTo<CalculateUtilitiesState>();
        }

        private void LoadLastData(UtilitiesContext coldWaterContext, UtilitiesContext hotWaterContext, ElectricPowerContext electricPowerContext)
        {
            using (var db = new AppDbContext())
            {
                var latestRecord = db.UtilitiesData
                    .Include(u => u.СoldWater)
                    .Include(u => u.HeatCarrier)
                    .Include(u => u.ThermalEnergy)
                    .Include(u => u.ElectricPower)
                    .OrderByDescending(u => u.Id)
                    .FirstOrDefault();

                if (latestRecord != null)
                {
                    coldWaterContext.PreviousReadings = latestRecord.СoldWater?.MeteringDeviceReadings ?? 0;
                    hotWaterContext.PreviousReadings = latestRecord.HeatCarrier?.MeteringDeviceReadings ?? 0;

                    electricPowerContext.PreviousReadingsDay = latestRecord.ElectricPower?.MeteringDeviceReadingsDay ?? 0;
                    electricPowerContext.PreviousReadingsNight = latestRecord.ElectricPower?.MeteringDeviceReadingsNight ?? 0;
                }
            }
        }

        private ElectricPowerContext TakeEPowerContext(int residents)
        {
            Console.Write("Введите текущие дневные показания ЭЭ: ");
            var currentReadingsDay = GetDecimal();
            Console.Write("Введите текущие ночные показания ЭЭ: ");
            var currentReadingsNight = GetDecimal();
            var electricPowerContext = new ElectricPowerContext()
            {
                Residents = residents,
                CurrentReadingsDay = currentReadingsDay,
                CurrentReadingsNight = currentReadingsNight,
                MeteringDevice = AskYesNo("Есть ли прибор учета для ЭЭ?")
            };
            return electricPowerContext;
        }

        private UtilitiesContext TakeHotWaterContext(int residents)
        {
            Console.Write("Введите текущие показания ГВС: ");
            var hotWaterContext = new UtilitiesContext()
            {
                Residents = residents,
                CurrentReadings = GetDecimal(),
                MeteringDevice = AskYesNo("Есть ли прибор учета для ГВС?")
            };
            return hotWaterContext;
        }

        private UtilitiesContext TakeColdWaterContext(int residents)
        {
            Console.Write("Введите текущие показания ХВС: ");
            var coldWaterContext = new UtilitiesContext()
            {
                Residents = residents,
                CurrentReadings = GetDecimal(),
                MeteringDevice = AskYesNo("Есть ли прибор учета для ХВС?")
            };
            return coldWaterContext;
        }

        private int TakeResidents()
        {
            Console.WriteLine("Введите количество проживающих:");
            int residents = GetInt();
            return residents;
        }
    }

    internal class CalculateUtilitiesState : UtilitiesState
    {
        public override void OnEnter(UtilitiesStateMachine machine)
        {
            base.OnEnter(machine);

            if (machine.UtilitiesContext == null)
                throw new NullReferenceException(nameof(machine.UtilitiesContext));

            var utilitiesfacade = new CalculteUtilitiesFacade(machine.UtilitiesContext);

            var result = utilitiesfacade.Calculate();
            machine.UtilitiesResult = result;

            machine.SwitchTo<OutputDataUtilitiesState>();
        }
    }

    internal class OutputDataUtilitiesState : UtilitiesState
    {
        public override void OnEnter(UtilitiesStateMachine machine)
        {
            base.OnEnter(machine);

            if (machine.UtilitiesResult == null)
                throw new NullReferenceException(nameof(machine.UtilitiesResult));

            if (machine.UtilitiesContext == null)
                throw new NullReferenceException(nameof(machine.UtilitiesContext));

            var output = machine.UtilitiesResult;
            var coldWater = output.ColdWaterResult;
            var hotWater = output.HotWaterResult;
            var electricPower = output.ElectricPowerResult;
            var hcOutput = hotWater.HeatCarrierResult;
            var teOutput = hotWater.ThermalEnergyUtilitiesResult;

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Холодная вода ");
            stringBuilder.AppendLine($"-Итоговая цена: {coldWater.UtilitiesCost} ");
            stringBuilder.AppendLine($"-Объём: {coldWater.Volume}");
            stringBuilder.AppendLine($"Горячая вода ");
            stringBuilder.AppendLine($"-Итоговая цена: {hotWater.UtilitiesCost} ");
            stringBuilder.AppendLine("ГВС Теплоноситель");
            stringBuilder.AppendLine($"-Цена: {hcOutput.UtilitiesCost} ");
            stringBuilder.AppendLine($"-Объём: {hcOutput.Volume}");
            stringBuilder.AppendLine("ГВС Тепловая энергия");
            stringBuilder.AppendLine($"-Цена: {teOutput.UtilitiesCost} ");
            stringBuilder.AppendLine($"-Объём: {teOutput.Volume}");
            stringBuilder.AppendLine($"Электроэнергия ");
            stringBuilder.AppendLine($"-Итоговая цена: {electricPower.UtilitiesCost} ");
            stringBuilder.AppendLine($"-Объём: {electricPower.Volume}");

            if (machine.UtilitiesContext.ElectricPowerContext.MeteringDevice)
            {
                stringBuilder.AppendLine($"Электроэнергия День");
                stringBuilder.AppendLine($"-Цена: {electricPower.UtilitiesCostDay} ");
                stringBuilder.AppendLine($"-Объём: {electricPower.VolumeDay}");
                stringBuilder.AppendLine($"Электроэнергия День");
                stringBuilder.AppendLine($"-Цена: {electricPower.UtilitiesCostNight} ");
                stringBuilder.AppendLine($"-Объём: {electricPower.VolumeNight}");
            }

            Console.Write(stringBuilder.ToString());

            machine.SwitchTo<SaveDataUtilitiesState>();
        }
    }

    internal class SaveDataUtilitiesState : UtilitiesState
    {
        public override void OnEnter(UtilitiesStateMachine machine)
        {
            base.OnEnter(machine);

            if (machine.UtilitiesResult == null)
                throw new NullReferenceException(nameof(machine.UtilitiesResult));

            if (machine.UtilitiesContext == null)
                throw new NullReferenceException(nameof(machine.UtilitiesContext));

            if (AskYesNo("Сохранить результат?"))
            {
                using (var db = new AppDbContext())
                {
                    var model = UtilitiesData.CreateModel(machine.UtilitiesResult, machine.UtilitiesContext);
                    db.UtilitiesData.Add(model);
                    db.SaveChanges();
                }
            }
            if (AskYesNo("Удалить всю базу данных?"))
            {
                machine.SwitchTo<ResetDataBaseUtilitiesState>();
            }
            else
            {
                machine.SwitchTo<InputDataUtilitiesState>();
            }
        }
    }

    internal class ResetDataBaseUtilitiesState : UtilitiesState
    {
        public override void OnEnter(UtilitiesStateMachine machine)
        {
            base.OnEnter(machine);

            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

            machine.SwitchTo<InputDataUtilitiesState>();
        }
    }
}
