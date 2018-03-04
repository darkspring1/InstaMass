using SM.Domain.Persistent.EF.State;
using System;

namespace SM.Domain.Model
{
    public class SMTask
    {

        internal SMTask()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        internal int TypeId { get; set; }
        internal DateTime CreatedAt { get; set; }
        internal Guid AccountId { get; set; }
        public Account Account { get; set; }
        internal int Version { get; set; }
        internal int ExternalSystemVersion { get; set; }
    }
}
