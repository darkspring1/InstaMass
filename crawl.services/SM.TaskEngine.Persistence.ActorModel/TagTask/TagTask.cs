using SM.TaskEngine.Persistence.ActorModel.TagTask.Events;
using SM.TaskEngine.Persistence.ActorModel.TagTask.Commands;

namespace SM.TaskEngine.Persistence.ActorModel.TagTask
{
    public class TagTask : BasePersistanceActor<TagTaskState>
    {
        private TagTaskState _state;

        protected override TagTaskState State => _state;

        public TagTask()
        {
            _state = new TagTaskState();

            Recover<TagTaskCreated>(e =>
            {
                _state.Login = e.Login;
                _state.Tags = e.Tags;
                IncEventCounter();
            });

            Recover<TagTaskUpdated>(e =>
            {
                _state.Tags = e.Tags;
                IncEventCounter();
            });

            Executing();
        }

        protected override void Executing()
        {
            Command<CreateTagTask>(c => {
                var @event = new TagTaskCreated { Login = c.Login, Tags = c.Tags };
                Save(@event);
            });

            Command<UpdateTagTask>(c => {
                var @event = new TagTaskUpdated { Tags = c.Tags };
                Save(@event);
            });

            base.Executing();
        }
        
    }
}

