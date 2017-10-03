using Akka.Actor;
using Akka.Cluster.Sharding;
using Akka.Event;
using Akka.Persistence;
using InstaMass.ActorModel.Commands;
using InstaMass.ActorModel.Events;
using InstaMass.Api.ActorModel.Commands;
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
        private InstaUserActionActorState _state;
        private ICacheProvider _cacheProvider;

        private readonly ILoggingAdapter _logger = Context.GetLogger();

        int _eventCount = 0;        

        public InstaUserActionActor()
        {
            _cacheProvider = new MemoryCacheProvider();
            //PersistenceId = Context.Parent.Path.Name + "-" Self.Path.Name;
            PersistenceId = $"{DateTime.UtcNow.ToString("dd.MM.yyyy")}_{Self.Path.Name}";
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

        public override string PersistenceId { get; }

        private void Starting()
        {
            Command<InstaUserActionActorStart>(c => Start(c));
            /*
            var path = Context.Parent.Path.ToStringWithoutAddress();
            var sel = Context.ActorSelection(path + "/*");

            sel.Tell("Hello");

            Command<string>(c => {
                _logger.Debug(c);
                });
            */
        }

        private void Executing()
        {
            Command<InstaUserActionActor>(c => Log.Info($"{PersistenceId} is executing"));
            Command<SaveResults>(c => c.ExecutedActions != null && c.ExecutedActions.Any(),
                c => Save(c));

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


        private void Start(InstaUserActionActorStart m)
        {
            ActionStrategy s = new ActionStrategy(
                m.InstaLogin,
                m.InstaPassword, m.Tags, m.Actions, _state.ExecutedActions, _cacheProvider);
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
