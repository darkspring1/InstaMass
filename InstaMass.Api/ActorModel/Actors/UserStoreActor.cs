using Akka.Actor;
using Api.ActorModel.Commands;
using Api.ActorModel.Messages;
using InstaMass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.ActorModel.Actors
{
    public class UserStoreActor : ReceiveActor
    {
        public UserStoreActor()
        {
            Receive<GetInstaUsers>(m =>
            {
                var users = InstaUserRepository
                .Users
                .Skip(m.Skip)
                .Take(m.Take);


                Sender.Tell(new InstaUserInfoResponse(users));
            });
        }
    }
}
