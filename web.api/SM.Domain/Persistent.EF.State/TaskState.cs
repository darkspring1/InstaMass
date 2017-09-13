using System;
namespace SM.Domain.Persistent.EF.State
{
    public class TaskState
    {
        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid AccountId { get; set; }
        public AccountState Account { get; set; }
    }
}
