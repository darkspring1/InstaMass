using Akka.Actor;

namespace InstaMass.Api.ActorModel.Actors
{
    /// <summary>
    /// Top-level actor, all client messages will flow through this actor
    /// </summary>
    internal class APIActor : ReceiveActor
    {
       
        public APIActor()
        {
            Start();
        }

        private void Start()
        {
            Receive<string>(login =>
            {
                Sender.Tell("pong");
            });
        }
    }
}
