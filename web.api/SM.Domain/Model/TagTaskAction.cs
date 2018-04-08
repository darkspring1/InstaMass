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
        public string MediaPk { get; set; }

        public string MediaCode { get; set; }

        public string MediaUrl { get; set; }

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
                MediaPk = dto.MediaPk,
                MediaCode = dto.MediaCode,
                MediaUrl = $"https://www.instagram.com/p/{dto.MediaCode}/",
                Tag = dto.Tag,
            };
        }
    }
}
