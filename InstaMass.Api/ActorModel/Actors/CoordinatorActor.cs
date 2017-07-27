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
                    if (Context.Child(u.Login) != ActorRefs.Nobody)
                    {
                        _logger.Debug($"{u.Login} already exists");
                    }
                    else
                    {
                        //var userActionActor = Context.ActorOf(Props.Create(() => new InstaUserActionSingletonActor(u)), u.Login);
                        //userActionActor.Tell(StartExecuting.Instance);

                        var message = new InstaUserActionActorStart(u.Login, u.Password, u.Tags, u.Actions);
                        _shardRegion.Tell(new ShardEnvelope(u.Login, message));
                    }
                }
                ContinueExecuting();
            });
        }

        Task KillChildren(IActorRef[] children, int number)
        {
            _logger.Debug($"{number} will be killed");
            LinkedList<Task> stopTasks = new LinkedList<Task>();
            for (int i = 0; i < number; i++)
            {
                stopTasks.AddLast(children[i].GracefulStop(TimeSpan.FromSeconds(5)));
            }
            return Task.WhenAll(stopTasks);
            //_logger.Debug($"{number} children were killed");
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
