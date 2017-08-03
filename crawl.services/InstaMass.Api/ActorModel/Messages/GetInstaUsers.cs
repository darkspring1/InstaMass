
namespace Api.ActorModel.Commands
{
    public class GetInstaUsers
    {
        public int Skip { get; }
        public int Take { get; }
        public GetInstaUsers(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
