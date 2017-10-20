using Akka.Actor;
using Akka.Cluster.Sharding;
using SM.TaskEngine.Api.ActorModel.Commands;
using SM.TaskEngine.Common.Sharding;
using SM.TaskEngine.Persistence.ActorModel.InstagramAccount;
using SM.TaskEngine.Persistence.ActorModel.TagTask;
using System;

namespace SM.TaskEngine.Api.ActorModel
{
    public class ApiMaster : ReceiveActor
    {
        IActorRef _instagramAccount;
        IActorRef _tagTask;

        public ApiMaster()
        {
            _instagramAccount = CreateShardProxy<InstagramAccount>();
            _tagTask = CreateShardProxy<TagTask>();
            Executing();
        }


        private IActorRef CreateShardProxy<TActor>()
        {
            return ClusterSharding.Get(Program.System).StartProxy(
                typeName: typeof(TActor).Name,
                role: "persistence",
                messageExtractor: new MessageExtractor());
        }

        private void Executing()
        {
            Receive<string>(c => {
                Console.WriteLine($"{Self.Path.Name} {c}");
            });

            Receive<UpdateAccount>(c => {
                _instagramAccount.Tell(new ShardEnvelope(c.Login, new Persistence.ActorModel.InstagramAccount.Commands.UpdateAccount(c.Version, c.Password)), Sender);
            });


            Receive<CreateTagTask>(c => {
                _instagramAccount.Tell(new ShardEnvelope(c.Id, new Persistence.ActorModel.TagTask.Commands.CreateTagTask(c.Version, c.Login, c.Tags )), Sender);
            });

            Receive<UpdateTagTask>(c => {
                _instagramAccount.Tell(new ShardEnvelope(c.Id, new Persistence.ActorModel.TagTask.Commands.UpdateTagTask(c.Version, c.Tags )), Sender);
            });
        }


        
    }
}
