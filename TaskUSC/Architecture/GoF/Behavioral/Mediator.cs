using System.Collections.Generic;

namespace TaskUSC.Architecture.GoF.Behavioral
{
    public class Mediator<T>
    {
        private HashSet<IReceiver<T>> receivers = new HashSet<IReceiver<T>>();
        public void AddReceiver(IReceiver<T> receiver) => OnAddReceiver(receiver);
        public void RemoveReceiver(IReceiver<T> receiver) => OnRemoveReceiver(receiver);

		public virtual void Send(IColleague<T> trigger, T context)
        {
            foreach (var colleague in receivers)
                colleague.Notify(context);
        }

        public virtual void SendFrom(IColleague<T> trigger, IReceiver<T> from, T context)
        {
            if (receivers.Contains(from))
                from.Notify(context);
        }

        protected virtual void OnAddReceiver(IReceiver<T> receiver)
        {
            receivers.Add(receiver);
        }

		protected virtual void OnRemoveReceiver(IReceiver<T> receiver)
		{
			receivers.Remove(receiver);
		}
	}
}
