using SM.Domain.Persistent.EF.State;

namespace SM.Domain.Model
{
    public class LikeTask: Entity<LikeTaskState>
    {
        internal LikeTask(LikeTaskState state) : base(state)
        {
            
        }
    }
}
