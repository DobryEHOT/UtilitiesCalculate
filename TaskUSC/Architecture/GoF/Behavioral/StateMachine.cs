using System.Collections.Generic;
using System;
using System.Linq;

namespace TaskUSC.Architecture.GoF.Behavioral
{
    //public abstract class StateMachine<M> where M : StateMachine<M>
    //{
    //    public State<M> ActiveState { get; private set; }

    //    private HashSet<State<M>> states = new HashSet<State<M>>();

    //    public StateMachine()
    //    {
    //        states.Add(new NoneState<M>());
    //        ActiveState = FindState<NoneState<M>>();
    //        InicializeMachine();
    //    }

    //    public void CloseManipulator() => ActiveState.OnExit((M)this);

    //    public void DoUpdate() => ActiveState.OnUpdate((M)this);

    //    public void SwichTo<T>() where T : State<M>
    //    {
    //        if (ActiveState == null)
    //        {
    //            var state = FindState<T>();
    //            ActiveState = state;
    //            ActiveState.OnEnter((M)this);
    //        }

    //        if (ActiveState is T)
    //            return;

    //        var nextState = FindState<T>();
    //        ActiveState.OnExit((M)this);
    //        ActiveState = nextState;
    //        nextState.OnEnter((M)this);
    //    }

    //    public void SwichToState<T>(T state) where T : State<M>
    //    {
    //        if (!states.Contains(state))
    //            return;

    //        if (ActiveState == null)
    //        {
    //            ActiveState = state;
    //            ActiveState.OnEnter((M)this);
    //        }

    //        if (ActiveState == state)
    //            return;

    //        var nextState = state;
    //        ActiveState.OnExit((M)this);
    //        ActiveState = nextState;
    //        nextState.OnEnter((M)this);
    //    }

    //    protected abstract void InicializeMachine();

    //    protected void AddState(State<M> state) => states.Add(state);

    //    protected State<M> FindState<T>() where T : State<M>
    //    {
    //        foreach (var state in states)
    //            if (state is T)
    //                return state;

    //        throw new Exception($"StateManipulator dosen't have state {typeof(T).Name}");
    //    }
    //}

    public abstract class StateMachine<M> where M : StateMachine<M>
    {
        public State<M> ActiveState { get; private set; }
        private State<M>? nextState;
        private HashSet<State<M>> states = new HashSet<State<M>>();
        private bool isRunning = false;

        public StateMachine()
        {
            states.Add(new NoneState<M>());
            ActiveState = FindState<NoneState<M>>();
            InicializeMachine();
        }

        public void Run()
        {
            if (!isRunning)
            {
                isRunning = true;
            }
            else
            {
                return;
            }

            while (isRunning)
            {
                if (nextState != null && nextState != ActiveState)
                {
                    if (ActiveState != null)
                        ActiveState.OnExit((M)this);

                    var oldState = ActiveState;
                    ActiveState = nextState;
                    nextState = null;
                    ActiveState.OnEnter((M)this);
                }

                if (ActiveState != null)
                    ActiveState.OnUpdate((M)this);
            }
        }

        public void CloseManipulator()
        {
            isRunning = false;
            if (ActiveState != null)
                ActiveState.OnExit((M)this);
        }

        public void SwitchTo<T>() where T : State<M>
        {
            var state = FindState<T>();
            if (state != null && state != ActiveState && state != nextState)
                nextState = state;
        }

        public void SwitchToState<T>(T state) where T : State<M>
        {
            if (states.Contains(state) && state != ActiveState && state != nextState)
                nextState = state;
        }

        protected abstract void InicializeMachine();

        protected void AddState(State<M> state) => states.Add(state);

        protected State<M> FindState<T>() where T : State<M>
        {
            foreach (var state in states)
                if (state is T target)
                    return target;

            throw new Exception($"StateManipulator doesn't have state {typeof(T).Name}");
        }
    }
}
