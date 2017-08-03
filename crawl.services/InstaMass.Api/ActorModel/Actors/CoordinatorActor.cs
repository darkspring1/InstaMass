using Akka.Actor;
using Akka.Event;
using Api.ActorModel.Commands;
using Api.ActorModel.Messages;
using InstaMass.ActorModel.Commands;
using InstaMass.Api.ActorModel.Commands;
using InstaMass.Api.Sharding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    public class CoordinatorActor : ReceiveActor
    {
        const int _take = 100;
        CoordinatorState _state;
        IActorRef _userStoreActor;
        IActorRef _shardRegion;
        ILoggingAdapter _logger = Context.GetLogger();

        const int maxChild = 2;

        public CoordinatorActor(IActorRef userStoreActor, IActorRef shardRegion)
        {
            _userStoreActor = userStoreActor;
            _shardRegion = shardRegion;

            _state = new CoordinatorState(0);

            Receive<StartExecuting>(m =>
            {
                _userStoreActor.Tell(new GetInstaUsers(_state.Skip, _take));
            });

            Receive<InstaUserInfoResponse>(m =>
            {
                foreach (var u in m.Users)
                {
                    var message = new InstaUserActionActorStart(u.Login, u.Password, u.Tags, u.Actions);
                    _shardRegion.Tell(new ShardEnvelope(u.Login, message));
                }
                ContinueExecuting();
            });
        }

        private void ContinueExecuting()
        {
            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(2), Self, StartExecuting.Instance, Self);
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
