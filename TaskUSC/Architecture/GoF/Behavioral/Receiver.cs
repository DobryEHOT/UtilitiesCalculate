using TaskUSC.Architecture.GoF.Creational;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskUSC.Architecture.GoF.Behavioral
{
	public interface IReceiverBase { }

	public interface IReceiver<T> : IReceiverBase
	{
		void Notify(T context);
	}

	public class ReceiverAdapter
	{
		IReceiverBase send;
		public ReceiverAdapter(IReceiverBase sendner) => send = sendner;
		public IReceiver<T> GetSendner<T>()
		{
			if (send is IReceiver<T> convert)
				return convert;

			throw new Exception($"You try get {send.GetType()} as {nameof(IReceiver<T>)}");
		}
	}

	public abstract class Receiver<T> : IReceiver<T>
	{
		protected string nameSendner = "None";
		void IReceiver<T>.Notify(T message) => Send(message);
		protected abstract void Send(T message);
	}

	public abstract class ReceiverDouble<T, Y> : Receiver<T>, IReceiver<Y>
	{
		protected string nameSecondSendner = "None";
		void IReceiver<Y>.Notify(Y message) => Send(message);
		protected abstract void Send(Y message);
	}
}
