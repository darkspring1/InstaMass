using SM.Domain.Persistent.EF.State;
using System;

namespace SM.Domain.Model
{
    public class TagTask: Entity<TagTaskState>
    {
         Lazy<string[]> _tagsLazy;

        internal TagTask(TagTaskState state) : base(state)
        {
            _tagsLazy = new Lazy<string[]>(() => (state.Tags ?? "").Split(','));
        }

        public Guid Id => State.TaskId;

        public int Version => State.Task.Version;

        public int ExternalSystemVersion
        {
            get
            {
                return State.Task.ExternalSystemVersion;
            }

            private set
            {
                State.Task.ExternalSystemVersion = value;
            }
        }

        public string Login => State.Task.Account.Login;

        public string[] Tags => _tagsLazy.Value;

        

        /// <summary>
        /// Устанавливает новое значений ExternalSystemVersion.
        /// Новое значение должно быть больше, текущего значения ExternalSystemVersion.
        /// </summary>
        /// <param name="newExternalSystemVersionValue"></param>
        public void InceraseExternalSystemVersion(int newExternalSystemVersionValue)
        {
            if (ExternalSystemVersion < newExternalSystemVersionValue)
            {
                ExternalSystemVersion = newExternalSystemVersionValue;
            }
        }

        public static TagTask Create(Guid accountId, string[] tags)
        {
            var tagsStr = string.Join(",", tags);
            TaskState baseTaskState = new TaskState
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                TypeId = (int)TaskTypeEnum.Like,
                CreatedAt = DateTime.UtcNow,
                Version = 1,
                ExternalSystemVersion = 0
            };

            TagTaskState state = new TagTaskState
            {
                Task = baseTaskState,
                Tags = tagsStr
            };

            return new TagTask(state);
        }


    }
}
