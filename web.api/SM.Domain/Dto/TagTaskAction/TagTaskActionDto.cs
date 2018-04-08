using System;

namespace SM.Domain.Dto.TagTaskAction
{
    public class TagTaskActionDto
    {

        public Guid TaskId { get; set; }

        /// <summary>
        /// время начала дейсвия в UTC
        /// </summary>
        public DateTime StartedAt { get; set; }

        /// <summary>
        /// время завершения действия в UTC
        /// </summary>
        public DateTime FinishedAt { get; set; }

        // <summary>
        /// таг по которому нашли Media
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Id media что лайкнули
        /// </summary>
        public string MediaId { get; set; }

        public bool IsSuccess { get; set; }
    }
}
