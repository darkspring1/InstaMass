using Akka.Actor;
using Akka.Cluster.Tools.Singleton;
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
    public class InstaUserActionSingletonActor : ReceiveActor
    {
        private readonly InstaUserInfo _instaUserInfo;

        ILoggingAdapter _logger = Context.GetLogger();

        public InstaUserActionSingletonActor(InstaUserInfo instaUserInfo)
        {
            _instaUserInfo = instaUserInfo;

            Receive<StartExecuting>(m =>
            {
                CreateUserActionActor(_instaUserInfo)
                .Tell(StartExecuting.Instance);
            });

            Receive<InstaUserActionActorFinished>(m => {
                _logger.Debug($"{m.Name} finished");
                Self.Tell(PoisonPill.Instance);
            });

        }
     
        private IActorRef CreateUserActionActor(InstaUserInfo u)
        {
            var instaUserActionActorProps = Props.Create(() =>
                        new InstaUserActionActor(Self, u.Login, u.Password, DateTime.UtcNow,
                            new MemoryCacheProvider(),
                            u.Tags, u.Actions
                    ));

            var settings = ClusterSingletonManagerSettings.Create(Context.System);

            var proxyName = $"InstaUserActionSingletonProxy::{u.Login}";
            var managerName = $"InstaUserActionSingletonManager:{u.Login}";

            var manager = Context.ActorOf(ClusterSingletonManager.Props(
                singletonProps: instaUserActionActorProps,         // Props used to create actor singleton
                terminationMessage: PoisonPill.Instance,                  // message used to stop actor gracefully
                settings: settings),// cluster singleton manager settings
                name: managerName);

            var proxy = Context.ActorOf(ClusterSingletonProxy.Props(
                singletonManagerPath: manager.Path.ToStringWithoutAddress(),                  // corresponding singleton manager name
                settings: ClusterSingletonProxySettings.Create(Context.System)),// singleton proxy settings
                name: proxyName);

            return proxy;

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
