using System;

namespace TaskUSC.Architecture.GoF.Structural
{
    internal abstract class SaveUseDecorator<Y>
    {
        protected Y objDecorator;
        private string initErr = "First you need to initialize the instance";
        public virtual void Initialise(Y obj)
        {
            objDecorator = obj;
        }

        protected T SaveUse<T>(Func<T> action)
        {
            if (objDecorator.Equals(default(Y)))
                throw new Exception(initErr);

            return action();
        }

        protected void SaveUse(Action action)
        {
            if (objDecorator.Equals(default(Y)))
                throw new Exception(initErr);

            action();
        }
    }
}
