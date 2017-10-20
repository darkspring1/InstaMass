using Akka.Actor;
using SM.TaskEngine.Persistence.ActorModel.InstagramAccount.Commands;
using System;


namespace SM.TaskEngine.Api.TestClient
{
    public class Printer : ReceiveActor
    {

        public Printer()
        {
            Receive<SyncAccountVersion>(c =>
            {
                Console.WriteLine($"{c.Login} V {c.Version} was saved");
            });
        }

    }
}
