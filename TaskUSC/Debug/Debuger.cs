using TaskUSC.Architecture.GoF.Creational;

namespace TaskUSC.Debug
{
    public class Debuger : Singleton<Debuger>
    {
        private Action<string> action;
        public void SetLogMethod(Action<string> action)
        {
            lock (this)
            {
                this.action = action;
            }
        }

        public void WriteLine(string message) => action(message);
    }
}
