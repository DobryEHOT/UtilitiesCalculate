namespace TaskUSC.Architecture.GoF.Creational
{
	public class Singleton<T> where T : Singleton<T>, new()
	{
		private static T Value;
		private static readonly object lockObj = new object();
		public static T MainSingleton
		{
			get
			{
				lock (lockObj)
				{
					if (Value == null)
					{
						Value = new T();
						if (Value is IInitializable init)
							init.Initialise();
					}
				}

				return Value;
			}
		}

		public T SubstituteInstance(T instance)
		{
			var old = Value;
			Value = instance;
			return old;
		}
    }

	public interface IInitializable
	{
		void Initialise();
	}
}
