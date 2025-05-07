using TaskUSC.Architecture.GoF.Behavioral;

namespace TaskUSC.Architecture.GoF.Creational
{
	public abstract class Builder<B, C, U>
		where B : IBuild<U>, new()
	{
		private B build;
		protected abstract Dictionary<Type, IStratage<C>> buildStratages { get; set; }
		public Builder() => build = new B();
		public virtual B GetBuild() => build;

		public T AppendBuildElement<T>() where T : U, new()
		{
			T data;
			data = new T();
			build.AddDetail<U>(data);
			return data;
		}

		public void AppendBuildElement<T>(T data) where T : U
		{
			build.AddDetail<U>(data);
		}

		public void Build<T>(C context) where T : IStratage<C>
		{
			IStratage<C> stratage;
			if (buildStratages.TryGetValue(typeof(T), out stratage))
				stratage.DoStratage(context);
		}
	}

	public interface IBuild<T>
	{
		void AddDetail<D>(D detail) where D : T;
		bool ContainDetail<D>() where D : T;
		void RemoveDetail<D>(D detail) where D : T;
	}

	public abstract class BuildStratageContext<B, T, U> : IStratage<T> where B : IBuild<U>, new()
	{
		protected readonly Builder<B, T, U> builder;
		public BuildStratageContext(Builder<B, T, U> builder) => this.builder = builder;

		public void DoStratage(T context) => OnStratage(context);

		protected abstract void OnStratage(T context);
	}
}