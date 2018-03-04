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
            _tagsLazy = PropertyFromJsonLazy<string[]>(TagsJson);
            _lastPost = PropertyFromJsonLazy<SwitchedProperty>(LastPostJson);
            _posts = PropertyFromJsonLazy<SwitchedRange>(PostsJson);
            _followers = PropertyFromJsonLazy<SwitchedRange>(FollowersJson);
            _followings = PropertyFromJsonLazy<SwitchedRange>(FollowingsJson);
        }

        private Lazy<T> PropertyFromJsonLazy<T>(string json) => new Lazy<T>(() => PropertyFromJson<T>(json));

        static private T PropertyFromJson<T>(string json) => JsonConvert.DeserializeObject<T>(json);
        static private string PropertyToJson(object property) => JsonConvert.SerializeObject(property);

        public Guid Id => TaskId;

        internal Guid TaskId { get; set; }

        public int Version { get; private set; }

        private int ExternalSystemVersion { get; set; }

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
                TypeId = (int)TaskTypeEnum.Like,
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

        SMTask Task { get; set; }

        public bool AvatarExistDisabled { get; private set; }

        internal  string TagsJson { get; set; }

        internal string LastPostJson { get; set; }

        internal string PostsJson { get; set; }
        internal string FollowersJson { get; set; }
        internal string FollowingsJson { get; set; }


    }
}
