using Akka.Actor;
using Akka.Actor.Dsl;
using Akka.Event;
using Api.ActorModel.Commands;
using Api.ActorModel.Messages;
using InstaMass.ActorModel.Actors;
using InstaMass.ActorModel.Commands;
using InstaMfl.Core.Cache;
using System;

namespace Api.ActorModel.Actors
{
    public class CoordinatorState
    {
        public int Skip { get; }

        public CoordinatorState(int skip)
        {
            Skip = skip;
        }
    }

    public class Kill { }

    public class CoordinatorActor : ReceiveActor
    {
        const int _take = 100;
        CoordinatorState _state;
        IActorRef _userStoreActor;
        ILoggingAdapter _logger = Context.GetLogger();

        int _startedActors = 0;

        public CoordinatorActor(IActorRef userStoreActor)
        {
            _userStoreActor = userStoreActor;
            _state = new CoordinatorState(0);

            Receive<StartExecuting>(m =>
            {
                _userStoreActor.Tell(new GetInstaUsers(_state.Skip, _take));
            });

            Receive<InstaUserInfoResponse>(m =>
            {
                foreach (var u in m.Users)
                {
                    var userActor = Context.ActorOf(Props.Create(() =>
                    new InstaUserActionActor(u.Login, u.Password, DateTime.UtcNow,
                        new MemoryCacheProvider(),
                        u.Tags, u.Actions
                    )), $"InstaUserActionActor:{u.Login}");

                    userActor.Tell(StartExecuting.Instance);
                    _startedActors++;
                }
            });

            Receive<InstaUserActionActorFinished>(m => {
                _startedActors--;
                _logger.Debug($"{m.Name} finished");
               
            });
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
