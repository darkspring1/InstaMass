using Newtonsoft.Json;
using System;
namespace SM.Domain.Model
{
    public class TagTask
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

        public static TagTask Create(Guid accountId,
            string[] tags,
            bool avatarExistDisabled,
            SwitchedProperty lastPost,
            SwitchedRange posts,
            SwitchedRange followers,
            SwitchedRange followings)
        {
            /*
            ThrowIfArgumentNull(tags, "tags");
            ThrowIfArgumentNull(lastPost, "lastPost");
            ThrowIfArgumentNull(posts, "posts");
            ThrowIfArgumentNull(followers, "followers");
            ThrowIfArgumentNull(followings, "followings");
            */

            SMTask baseTaskState = new SMTask
            {
                AccountId = accountId,
                TypeId = SMTask.TaskTypes.Like,
                CreatedAt = DateTime.UtcNow,
                Version = 1,
                ExternalSystemVersion = 0
            };

            return new TagTask
            {
                Task = baseTaskState,
                AvatarExistDisabled = avatarExistDisabled,
                TagsJson = PropertyToJson(tags),
                LastPostJson = PropertyToJson(lastPost),
                FollowersJson = PropertyToJson(followers),
                FollowingsJson = PropertyToJson(followings),
                PostsJson = PropertyToJson(posts),
            };
        }

        internal SMTask Task { get; set; }

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
