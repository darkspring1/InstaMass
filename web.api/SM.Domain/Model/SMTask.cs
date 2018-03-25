using SM.Domain.Persistent.EF.State;
using System;

namespace SM.Domain.Model
{
    public class SMTask
    {

        internal static class TaskTypes
        {
            public static int Tag => 1;
        }

        internal static class TaskStatuses
        {
            /// <summary>
            /// активная задача
            /// </summary>
            public static int Active => 1;

            /// <summary>
            /// задача удалена
            /// </summary>
            public static int Delete => 2;
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
