using System;
using Akka.Actor;
using Akka.Event;
using Akka.Persistence;
using SM.TaskEngine.Persistence.ActorModel.InstagramAccount.Commands;
using SM.TaskEngine.Persistence.ActorModel.InstagramAccount.Events;
using Akka.Cluster.Sharding;

namespace SM.TaskEngine.Persistence.ActorModel.InstagramAccount
{
    public class InstagramAccountState
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class InstagramAccount : ReceivePersistentActor
    {
        private InstagramAccountState _state;
        

        private readonly ILoggingAdapter _logger = Context.GetLogger();

        int _eventCount = 0;

        const int safeSnapshotAfter = 5;

        public InstagramAccount()
        {
            //PersistenceId = Context.Parent.Path.Name + "-" Self.Path.Name;
            PersistenceId = Self.Path.Name;
            _state = new InstagramAccountState();

            Recover<AccountUpdated>(e =>
            {
                _state.Login = e.Login;
                _state.Password = e.Password;
                _eventCount++;
            });

            Executing();
        }

        public override string PersistenceId { get; }

        private void Executing()
        {
            Command<UpdateAccount>(c => {
                _state.Login = c.Login;
                _state.Password = c.Password;
                Save(c); } );

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


        private void Save(UpdateAccount command)
        {
            var @event = new AccountUpdated(command.Login, command.Password);
            Persist(@event, e =>
            {
                _eventCount++;
                if (_eventCount > safeSnapshotAfter)
                {
                    SaveSnapshot(_state);
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

