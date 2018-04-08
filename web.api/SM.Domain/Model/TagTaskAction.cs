using SM.Domain.Dto.TagTaskAction;
using System;

namespace SM.Domain.Model
{
    public class TagTaskAction
    {

        internal TaskAction Action { get; set; }
        public Guid Id => Action.Id;

        internal Guid ActionId { get; set; }

        /// <summary>
        /// таг по которому нашли Media
        /// </summary>
        internal string Tag { get; set; }

        /// <summary>
        /// Id media что лайкнули
        /// </summary>
        internal string MediaId { get; set; }

        public static TagTaskAction Create(TagTaskActionDto dto)
        {
            var action = new TaskAction
            {
                TaskId = dto.TaskId,
                StartedAt = dto.StartedAt,
                FinishedAt = dto.FinishedAt,
                IsSuccess = dto.IsSuccess
            };

            return new TagTaskAction
            {
                Action = action,
                MediaId = dto.MediaId,
                Tag = dto.Tag,
            };
        }
    }
}
