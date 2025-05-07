namespace TaskUSC.Architecture.GoF.Behavioral
{
    public abstract class Colleague<T> : IColleague<T>
	{
        private Mediator<T> mediator;
        private IColleague<T> colleague;

        protected Colleague(Mediator<T> mediator)
        {
            if (mediator == null)
                throw new System.ArgumentNullException("mediator");
            this.mediator = mediator;
            mediator.AddReceiver(this);
            colleague = this;
        }

		protected virtual void RemoveReceiver()
		{
            mediator.RemoveReceiver(colleague);
		}

		protected void SetMediator(Mediator<T> mediator) => this.mediator = mediator;

        protected void SendMessage(T context) => colleague.SendMessage(context);

        protected void SendMessageFrom(IColleague<T> fromColleague, T context) => colleague.SendMessageFrom(fromColleague, context);

        public virtual void Notify(T context) { }

        void IColleague<T>.SendMessage(T context) => mediator.Send(this, context);

        void IColleague<T>.SendMessageFrom(IColleague<T> fromColleague, T context) => mediator.SendFrom(this, fromColleague, context);
    }

    public abstract class ColleagueDouble<T, Y> : Colleague<T>, IColleague<Y>
    {
        private Mediator<Y> mediator;
        private IColleague<Y> colleague;

        protected ColleagueDouble(Mediator<T> mediatorFirst, Mediator<Y> mediatorSecond) : base(mediatorFirst)
        {
            if (mediatorSecond == null)
                throw new System.ArgumentNullException("mediatorSecond");
            this.mediator = mediatorSecond;

            this.mediator.AddReceiver(this);
            colleague = this;
        }

		protected override void RemoveReceiver()
		{
			mediator.RemoveReceiver(colleague);
			base.RemoveReceiver();
		}

		protected void SendMessage(Y context) => colleague.SendMessage(context);

        protected void SendMessageFrom(IColleague<Y> fromColleague, Y context) => colleague.SendMessageFrom(fromColleague, context);

        public virtual void Notify(Y context) { }

        void IColleague<Y>.SendMessage(Y context) => mediator.Send(this, context);

        void IColleague<Y>.SendMessageFrom(IColleague<Y> fromColleague, Y context) => mediator.SendFrom(this, fromColleague, context);

    }

    public interface IColleague<T> : IReceiver<T>
	{
        void SendMessage(T context);

        void SendMessageFrom(IColleague<T> colleague, T context);
    }
}
