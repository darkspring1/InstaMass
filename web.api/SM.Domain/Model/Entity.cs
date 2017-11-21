using System;

namespace SM.Domain.Model
{
    public abstract class Entity<TState>
    {
        internal TState State { get; private set; }

        protected Entity(TState state)
        {
            State = state;
        }

        private Entity() { }


        protected static void ThrowIfArgumentNull<T>(T arg, string argName) where T : class
        {
            if (arg == null)
            {
                throw new ArgumentNullException(argName);
            }
        }

    }
}
