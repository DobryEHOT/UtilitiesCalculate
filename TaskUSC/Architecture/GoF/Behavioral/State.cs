namespace TaskUSC.Architecture.GoF.Behavioral
{
    public class State<T> where T : StateMachine<T>//, new()
    {
        public virtual void OnEnter(T machine) { }
        public virtual void OnUpdate(T machine) { }
        public virtual void OnExit(T machine) { }
    }
    public class NoneState<T> : State<T> where T : StateMachine<T>//, new()
    {
        public override sealed void OnEnter(T machine) { }
        public override sealed void OnUpdate(T machine) { }
        public override sealed void OnExit(T machine) { }
    }
}
