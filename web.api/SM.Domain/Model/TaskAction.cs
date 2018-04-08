using System;

namespace SM.Domain.Model
{
    /// <summary>
    /// действие, которое совершается в рамках задачи
    /// </summary>
    public class TaskAction : IEntity<Guid>
    {

        internal TaskAction()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        internal Guid TaskId { get; set; }

        internal bool IsSuccess { get; set; }

        /// <summary>
        /// время начала дейсвия в UTC
        /// </summary>
        public DateTime StartedAt { get; internal set; }

        /// <summary>
        /// время завершения действия в UTC
        /// </summary>
        public DateTime FinishedAt { get; internal set; }
    }
}
