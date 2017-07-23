

namespace Api.ActorModel.Messages
{
    public class InstaUserActionActorFinished
    {

        public string Name { get; }
        public InstaUserActionActorFinished(string name)
        {
            Name = name;
        }
    }
}
