using Newtonsoft.Json;
using SM.Domain.Dto.TagTask;
using System;
namespace SM.Domain.Model
{

    public class TagTask : IEntity<Guid>
    {
        Lazy<string[]> _tagsLazy;
        Lazy<SwitchedProperty> _lastPost;
        Lazy<SwitchedRange> _posts;
        Lazy<SwitchedRange> _followers;
        Lazy<SwitchedRange> _followings;

        internal TagTask()
        {
            _tagsLazy = PropertyFromJsonLazy<string[]>(() => TagsJson);
            _lastPost = PropertyFromJsonLazy<SwitchedProperty>(() => LastPostJson);
            _posts = PropertyFromJsonLazy<SwitchedRange>(() => PostsJson);
            _followers = PropertyFromJsonLazy<SwitchedRange>(() => FollowersJson);
            _followings = PropertyFromJsonLazy<SwitchedRange>(() => FollowingsJson);
        }

        private Lazy<T> PropertyFromJsonLazy<T>(Func<string> property) => new Lazy<T>(() => PropertyFromJson<T>(property()));

        static private T PropertyFromJson<T>(string json) => JsonConvert.DeserializeObject<T>(json);
        static private string PropertyToJson(object property) => JsonConvert.SerializeObject(property);

        public Guid Id => Task.Id;

        internal Guid TaskId { get; set; }

        public string[] Tags => _tagsLazy.Value;

        public SwitchedProperty LastPost => _lastPost.Value;

        public SwitchedRange Post => _posts.Value;

        public SwitchedRange Followers => _followers.Value;

        public SwitchedRange Followings => _followings.Value;

        static void FromDto(TagTask tagTask, TagTaskDto dto)
        {
            tagTask.Task.AccountId = dto.AccountId;
            tagTask.AvatarExistDisabled = dto.AvatarExistDisabled;
            tagTask.TagsJson = PropertyToJson(dto.Tags);
            tagTask.LastPostJson = PropertyToJson(dto.LastPost);
            tagTask.FollowersJson = PropertyToJson(dto.Followers);
            tagTask.FollowingsJson = PropertyToJson(dto.Followings);
            tagTask.PostsJson = PropertyToJson(dto.Posts);
        }

        public static TagTask Create(TagTaskDto dto)
        {
            /*
            ThrowIfArgumentNull(tags, "tags");
            ThrowIfArgumentNull(lastPost, "lastPost");
            ThrowIfArgumentNull(posts, "posts");
            ThrowIfArgumentNull(followers, "followers");
            ThrowIfArgumentNull(followings, "followings");
            */

            SMTask baseTask = new SMTask
            {
                TypeId = SMTask.TaskTypes.Tag,
                CreatedAt = DateTime.UtcNow,
                Version = 1,
                ExternalSystemVersion = 0
            };

            var tagTask = new TagTask
            {
                Task = baseTask
            };
            FromDto(tagTask, dto);
            return tagTask;
        }

        public void Update(TagTaskDto dto)
        {
            FromDto(this, dto);
        }

        internal SMTask Task { get; set; }

        public Account Account => Task.Account;

        public int Version => Task.Version;

        public bool AvatarExistDisabled { get; private set; }

        internal  string TagsJson { get; set; }

        internal string LastPostJson { get; set; }

        internal string PostsJson { get; set; }
        internal string FollowersJson { get; set; }
        internal string FollowingsJson { get; set; }


        /// <summary>
        /// Устанавливает новое значений ExternalSystemVersion.
        /// Новое значение должно быть больше, текущего значения ExternalSystemVersion.
        /// </summary>
        /// <param name="newExternalSystemVersionValue"></param>
        public void InceraseExternalSystemVersion(int newExternalSystemVersionValue) => Task.InceraseExternalSystemVersion(newExternalSystemVersionValue);


    }
}
