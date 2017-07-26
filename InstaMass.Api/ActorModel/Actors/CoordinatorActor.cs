using Akka.Actor;
using Akka.Actor.Dsl;
using Akka.Cluster.Tools.Singleton;
using Akka.Event;
using Akka.Routing;
using Api.ActorModel.Commands;
using Api.ActorModel.Messages;
using InstaMass;
using InstaMass.ActorModel.Actors;
using InstaMass.ActorModel.Commands;
using InstaMfl.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
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
        ILoggingAdapter _logger = Context.GetLogger();

        const int maxChild = 2;

        public CoordinatorActor(IActorRef userStoreActor)
        {
            _userStoreActor = userStoreActor;
            _state = new CoordinatorState(0);

            Receive<StartExecuting>(m =>
            {
                var children = Context.GetChildren();
                var diff = maxChild - children.Count();

                if (diff >= 0)
                {
                    _userStoreActor.Tell(new GetInstaUsers(_state.Skip, _take));
                }
                else
                {
                    KillChildren(children.ToArray(), diff)
                    .ContinueWith(t => StartExecuting.Instance)
                    .PipeTo(Self);
                }
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
                        var userActionActor = Context.ActorOf(Props.Create(() => new InstaUserActionSingletonActor(u)), u.Login);
                        userActionActor.Tell(StartExecuting.Instance);
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
