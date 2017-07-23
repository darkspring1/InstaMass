using Akka.Actor;
using Akka.Persistence;
using InstaMass.ActorModel.Commands;
using InstaMass.ActorModel.Events;
using InstaMfl.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InstaMass.Api.ActorModel.Actors
{
    public class UserActionActorState
    {
        public LinkedList<InstaAction> ExecutedActions { get; }

        public UserActionActorState()
        {
             ExecutedActions = new LinkedList<InstaAction>();   
        }
    }

    public class UserActionActor : ReceivePersistentActor
    {
        
        private string _persistentId;
        private UserActorState _state;
        private ICacheProvider _cacheProvider;
        int _eventCount = 0;


        public UserActionActor(Guid id)
        {
            _persistentId = id.ToString();
            
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
            Command<FinishExecuting>(c => c.ExecutedActions != null && c.ExecutedActions.Any(),
                c => Finish(c));
        }


        private void Start()
        {
            ActionStrategy s = new ActionStrategy(_instaLogin, _instaPassword, _tags, _actions, _state.ExecutedActions, _cacheProvider);
            s
                .Execute()
                .ContinueWith(actions =>
                new FinishExecuting() { ExecutedActions = actions.Result })
                .PipeTo(Self);

            Become(Executing);
        }

        private void Finish(FinishExecuting command)
        {
            var @event = new ActionsExecuted(command.ExecutedActions);

            Persist(@event, e =>
            {
                _eventCount++;
                if (_eventCount >= 1)
                {
                    SaveSnapshot(_state);
                    _eventCount = 0;
                }

                foreach (var a in e.Actions)
                {
                    _state.ExecutedActions.AddLast(a);
                }

                Log.Debug(LogMessage("Is finished"));
            });
        }

        private string LogMessage(string message) => $"{PersistenceId}: {message}";
    }
}
