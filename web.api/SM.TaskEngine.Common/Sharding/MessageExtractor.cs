using Akka.Cluster.Sharding;

namespace SM.TaskEngine.Common.Sharding
{
    public sealed class MessageExtractor : HashCodeMessageExtractor
    {
        const int maxNumberOfNodes = 1000000;
        public MessageExtractor() : base(maxNumberOfNodes * 10) { }
        public MessageExtractor(int maxNumberOfShards) : base(maxNumberOfShards) { }
        public override string EntityId(object message) =>
            (message as ShardEnvelope)?.EntityId;
        public override object EntityMessage(object message) =>
            (message as ShardEnvelope)?.Payload;
    }
}
