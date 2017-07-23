using System.Collections.Generic;

namespace InstaMass.ActorModel.Events
{
    class ActionsExecuted
    {
        public IEnumerable<InstaAction> Actions { get; }


        public ActionsExecuted(IEnumerable<InstaAction> actions)
        {
            Actions = actions;
        }
    }
}
