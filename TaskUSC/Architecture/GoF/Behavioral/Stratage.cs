using System;
using System.Collections.Generic;
using System.Text;

namespace TaskUSC.Architecture.GoF.Behavioral
{
    public interface IStratage<T>
    {
        void DoStratage(T context);
    }
    public interface IStratageResult<T, Y>
    {
        Y DoStratage(T context);
    }
    public interface ITryStratageResult<T, Y>
    {
        bool TryDoStratage(T context, out Y result);
    }
    public interface ITryStratage<T>
    {
        bool TryDoStratage(T context);
    }
}
