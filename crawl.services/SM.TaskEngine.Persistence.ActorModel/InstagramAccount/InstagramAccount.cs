using SM.TaskEngine.Persistence.ActorModel.InstagramAccount.Commands;
using SM.TaskEngine.Persistence.ActorModel.InstagramAccount.Events;

namespace SM.TaskEngine.Persistence.ActorModel.InstagramAccount
{
    public class InstagramAccount : BasePersistanceActor<InstagramAccountState>
    {
        private InstagramAccountState _state;

        public InstagramAccount()
        {
            _state = new InstagramAccountState();
            Recover<AccountUpdated>(e =>
            {
                _state.Password = e.Password;
                IncEventCounter();
            });

            Executing();
        }

        protected override InstagramAccountState State => _state;

        protected override void Executing()
        {
            Command<UpdateAccount>(c =>
            {
                if (c.Password != _state.Password)
                {
                    _state.Password = c.Password;
                    Save(c);
                }
            });

            base.Executing();
        }
    }
}