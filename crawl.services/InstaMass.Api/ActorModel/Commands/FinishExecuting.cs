using System.Collections.Generic;

namespace InstaMass.ActorModel.Commands
{
    public class FinishExecuting
    {
        FinishExecuting() { }
        public static FinishExecuting Instance => new FinishExecuting();
    }
}
