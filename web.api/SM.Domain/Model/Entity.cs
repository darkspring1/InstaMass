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

        }
    }
