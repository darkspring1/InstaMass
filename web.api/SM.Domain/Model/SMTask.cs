using SM.Domain.Persistent.EF.State;
using System;

namespace SM.Domain.Model
{
    public class SMTask
    {

        internal static class TaskTypes
        {
            public static int Like => 1;
        }

        internal SMTask()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public int TypeId { get; set; }
        internal DateTime CreatedAt { get; set; }
        internal Guid AccountId { get; set; }
        public Account Account { get; set; }
        internal int Version { get; set; }
        internal int ExternalSystemVersion { get; set; }

        public int StatusId => 1; //пока захардкодил 1 - активна

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
