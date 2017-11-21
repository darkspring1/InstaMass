using Newtonsoft.Json;
using SM.Domain.Persistent.EF.State;
using System;

namespace SM.Domain.Model
{
    public class TagTask: Entity<TagTaskState>
    {
        Lazy<string[]> _tagsLazy;
        Lazy<SwitchedProperty> _lastPost;
        Lazy<SwitchedRange> _posts;
        Lazy<SwitchedRange> _followers;
        Lazy<SwitchedRange> _followings;

        internal TagTask(TagTaskState state) : base(state)
        {
            _tagsLazy = PropertyFromJsonLazy<string[]>(state.Tags);
            _lastPost = PropertyFromJsonLazy<SwitchedProperty>(state.LastPost);
            _posts = PropertyFromJsonLazy<SwitchedRange>(state.Posts);
            _followers = PropertyFromJsonLazy<SwitchedRange>(state.Followers);
            _followings = PropertyFromJsonLazy<SwitchedRange>(state.Followings);
        }

        private Lazy<T> PropertyFromJsonLazy<T>(string json) => new Lazy<T>(() => PropertyFromJson<T>(json));

        static private T PropertyFromJson<T>(string json) => JsonConvert.DeserializeObject<T>(json);
        static private string PropertyToJson(object property) => JsonConvert.SerializeObject(property);

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

        public static TagTask Create(Guid accountId,
            string[] tags,
            bool avatarExistDisabled,
            SwitchedProperty lastPost,
            SwitchedRange posts,
            SwitchedRange followers,
            SwitchedRange followings)
        {
            ThrowIfArgumentNull(tags, "tags");
            ThrowIfArgumentNull(lastPost, "lastPost");
            ThrowIfArgumentNull(posts, "posts");
            ThrowIfArgumentNull(followers, "followers");
            ThrowIfArgumentNull(followings, "followings");

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
                AvatarExistDisabled = avatarExistDisabled,
                Tags = PropertyToJson(tags),
                LastPost = PropertyToJson(lastPost),
                Followers = PropertyToJson(followers),
                Followings = PropertyToJson(followings),
                Posts = PropertyToJson(posts),
            };

            return new TagTask(state);
        }


    }
}
