using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TaskUSC.Architecture.GoF.Behavioral;
using TaskUSC.Architecture.GoF.Creational;
using TaskUSC.Data;
using TaskUSC.DatabaseContexts;
using TaskUSC.Debug;
using TaskUSC.Singletons;
using TaskUSC.States.Utilities;

namespace TaskUSC.States.Global
{
    internal class GlobalStateMachine : StateMachine<GlobalStateMachine>
    {
        protected override void InicializeMachine()
        {
            AddState(new InitialiseGlobalState());
            AddState(new ServiceTakeGlobalState());
            AddState(new UtilitesCalculateGlobalState());
            AddState(new ExitGlobalState());
            SwitchTo<InitialiseGlobalState>();
        }
    }

    internal abstract class GlobalState : State<GlobalStateMachine>
    {
        public override void OnEnter(GlobalStateMachine machine)
        {
            base.OnEnter(machine);
            Singleton<Debuger>.MainSingleton.WriteLine(machine.GetType().Name + " Enter to state " + this.GetType().Name);
        }
    }

    internal class InitialiseGlobalState : GlobalState
    {
        public override void OnEnter(GlobalStateMachine machine)
        {
            base.OnEnter(machine);

            Singleton<TariffNormative>.MainSingleton.TariffNormativeData
                = new TariffNormativeData(
                35.78m,
                4.85m,
                35.78m,
                4.01m,
                998.69m,
                0.05349m,
                4.9m,
                2.31m,
                164m,
                4.28m);

            Singleton<Meters>.MainSingleton.MetersData
                = new MetersData(
                    4294967295m, //uint
                    4294967295m,
                    4294967295m
                    );

            using (var db = new AppDbContext())
            {
                db.Database.Migrate();
            }

            machine.SwitchTo<ServiceTakeGlobalState>();
        }
    }

    internal class ServiceTakeGlobalState : GlobalState
    {
        public override void OnEnter(GlobalStateMachine machine)
        {
            base.OnEnter(machine);

            machine.SwitchTo<UtilitesCalculateGlobalState>();
        }
    }

    internal class UtilitesCalculateGlobalState : GlobalState
    {
        private StateMachine<UtilitiesStateMachine>? stateMachine;
        public override void OnEnter(GlobalStateMachine machine)
        {
            base.OnEnter(machine);
            stateMachine = new UtilitiesStateMachine();
            stateMachine.Run();
        }
    }

    internal class ExitGlobalState : GlobalState
    {

    }
}
