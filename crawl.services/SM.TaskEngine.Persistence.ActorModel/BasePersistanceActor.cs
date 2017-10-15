using System;
using Akka.Event;
using Akka.Persistence;
using Akka.Cluster.Sharding;
using Akka.Actor;

namespace SM.TaskEngine.Persistence.ActorModel
{
    public abstract class BasePersistanceActor<TState> : ReceivePersistentActor
    {
        private readonly ILoggingAdapter _logger = Context.GetLogger();

        int _eventCount = 0;

        const int safeSnapshotAfter = 5;

        public BasePersistanceActor()
        {
            PersistenceId = Self.Path.Name;
        }

        public override string PersistenceId { get; }

        protected abstract TState State { get; }

        protected void IncEventCounter()
        {
            _eventCount++;
        }

        protected virtual void Executing()
        {
            Command<SaveSnapshotSuccess>(c => Finish());
            Command<SaveSnapshotFailure>(c =>
            {
                _logger.Warning("Snapshot was't saved");
                Finish();
            });

            Command<string>(c => {
                _logger.Debug(c);
            });
        }

        protected void Save<TEvent>(TEvent @event)
        {
            Persist(@event, e =>
            {
                _eventCount++;
                if (_eventCount > safeSnapshotAfter)
                {
                    SaveSnapshot(State);
                    _eventCount = 0;
                }
            });
        }


        private void Finish()
        {
            Context.Parent.Tell(new Passivate(PoisonPill.Instance));
            //Become(Starting);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Error(reason.ToString());
            base.PreRestart(reason, message);
        }

        protected override void PostStop()
        {
            _logger.Debug("Stopped");
            base.PostStop();
        }

        protected override void PreStart()
        {
            _logger.Debug("Starting");
            base.PreStart();
        }
    }
}