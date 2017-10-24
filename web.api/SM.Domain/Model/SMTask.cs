using SM.Domain.Persistent.EF.State;

namespace SM.Domain.Model
{
    public class SMTask: Entity<TaskState>
    {
        internal SMTask(TaskState state) : base(state)
        {
            
        }
    }
}
