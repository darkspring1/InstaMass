using Akka.Actor;
using Akka.Event;
using Akka.Persistence;
using Api.ActorModel.Messages;
using InstaMass.ActorModel.Commands;
using InstaMass.ActorModel.Events;
using InstaMfl.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InstaMass.ActorModel.Actors
{
    public class InstaUserActionActorState
    {
        public LinkedList<InstaAction> ExecutedActions { get; }

        public InstaUserActionActorState()
        {
             ExecutedActions = new LinkedList<InstaAction>();
            
        }
    }

    public class InstaUserActionActor : ReceivePersistentActor
    {
        private string _instaLogin;
        private string _instaPassword;
        private DateTime _date;
        private string _persistentId;
        private InstaUserActionActorState _state;
        private ICacheProvider _cacheProvider;

        private readonly ILoggingAdapter _logger = Context.GetLogger();

        string[] _tags { get; }

        int _eventCount = 0;

        InstaUserActionType[] _actions { get; }

        public InstaUserActionActor(
            string instaLogin, string instaPassword,
            DateTime date,
            ICacheProvider cacheProvider,
            string[] tags,
            InstaUserActionType[] actions
            )
        {
            _cacheProvider = cacheProvider;
            _instaLogin = instaLogin;
            _instaPassword = instaPassword;
            _date = date.ToUniversalTime();
            _persistentId = $"{_date.ToString("dd.MM.yyyy")}_{_instaLogin}";

            _tags = tags;
            _actions = actions;

            _state = new InstaUserActionActorState();
            
            Recover<ActionsExecuted>(e =>
            {
                foreach (var a in e.Actions)
                {
                    _state.ExecutedActions.AddLast(a);
                }
                _eventCount++;
            });
            
            
            Recover<SnapshotOffer>(offer =>
            {
                _state = (InstaUserActionActorState)offer.Snapshot;
            });

            Starting();
            
        }

        public override string PersistenceId => _persistentId;


        private void Starting()
        {
            Command<StartExecuting>(c => Start());
        }

        private void Executing()
        {
            Command<StartExecuting>(c => Log.Info($"{PersistenceId} is executing"));
            Command<SaveResults>(c => c.ExecutedActions != null && c.ExecutedActions.Any(),
                c => Save(c));


            Command<SaveSnapshotSuccess>(c => Finish());
            Command<SaveSnapshotFailure>(c =>
            {
                _logger.Warning("Snapshot was't saved");
                Finish();
            });
        }


        private void Start()
        {
            ActionStrategy s = new ActionStrategy(_instaLogin, _instaPassword, _tags, _actions, _state.ExecutedActions, _cacheProvider);
            s
                .Execute()
                .ContinueWith(actions => new SaveResults(actions.Result))
                .PipeTo(Self);

            Become(Executing);
        }

        private void Save(SaveResults command)
        {
            var @event = new ActionsExecuted(command.ExecutedActions);

            Persist(@event, e =>
            {
                
                foreach (var a in e.Actions)
                {
                    _state.ExecutedActions.AddLast(a);
                }

                _eventCount++;
                SaveSnapshot(_state);
                _eventCount = 0;
                //_logger.Debug("Is finished");
            });
        }

        private void Finish()
        {
            Context.Parent.Tell(new InstaUserActionActorFinished(Self.Path.Name));
            Self.Tell(PoisonPill.Instance);
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
