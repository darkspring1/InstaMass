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

namespace Api.ActorModel.Actors
{
    

    public class SingletonActor : ReceiveActor
    {
        ILoggingAdapter _logger = Context.GetLogger();

        public SingletonActor()
        {

            Receive<StartExecuting>(m =>
            {
                _logger.Debug("Hello");
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
