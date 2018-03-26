using SM.Domain.ConstantValues;
using System;

namespace SM.Domain.Model
{
    public class SMTask : IEntity<Guid>
    {

        internal static class TaskTypes
        {
            public static int Tag => 1;
        }

        internal SMTask()
        {
            Id = Guid.NewGuid();
        }

        public void MarkAsDeleted()
        {
            EntityStatusId = EntityStatuses.Deleted;
        }

        public Guid Id { get; private set; }
        public int TypeId { get; set; }
        internal DateTime CreatedAt { get; set; }
        internal Guid AccountId { get; set; }
        public Account Account { get; set; }
        internal int Version { get; set; }
        internal int ExternalSystemVersion { get; set; }
        internal int EntityStatusId { get; set; }
        public int TaskStatusId => 1; //пока захардкодил 1 - активна

        /// <summary>
        /// Устанавливает новое значений ExternalSystemVersion.
        /// Новое значение должно быть больше, текущего значения ExternalSystemVersion.
        /// </summary>
        /// <param name="newExternalSystemVersionValue"></param>
        internal void InceraseExternalSystemVersion(int newExternalSystemVersionValue)
        {
            if (ExternalSystemVersion < newExternalSystemVersionValue)
            {
                ExternalSystemVersion = newExternalSystemVersionValue;
            }
        }
    }
}
