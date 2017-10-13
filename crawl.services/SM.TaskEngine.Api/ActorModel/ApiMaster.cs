using Akka.Actor;
using System;

namespace SM.TaskEngine.Api.ActorModel
{
    public class ApiMaster : ReceiveActor
    {
        public ApiMaster()
        {
            Executing();
        }

        private void Executing()
        {
            Receive<string>(c => {
                Console.WriteLine($"{Self.Path.Name} {c}");
            });
        }
    }
}
