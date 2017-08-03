using System.Collections.Generic;

namespace InstaMass.ActorModel.Commands
{
    public class SaveResults
    {

        public SaveResults(IEnumerable<InstaAction> executedActions)
        {
            ExecutedActions = executedActions;
        }
        public IEnumerable<InstaAction> ExecutedActions { get; }
    }
}
