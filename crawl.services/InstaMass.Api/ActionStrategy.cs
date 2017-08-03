using InstaMfl.Core.Cache;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using InstaSharper.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaMass
{

    class Logger : ILogger
    {
        public void Write(string logMessage)
        {
            Console.WriteLine(logMessage);
        }
    }

    class ActionStrategy
    {
        const int TotalActionsPerHour  = 100;

        /// <summary>
        /// минимальное время между действиями (сек)
        /// </summary>
        TimeSpan MinTimeBetweenActions = TimeSpan.FromSeconds(5);

        const int MaxActionPerTime = 2;

        const int MaxLikesPerHour = 60;

        const int MaxSubscribesPerHour = 60;

        Random _random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// соотношение между количетвом действия, каждого типа
        /// </summary>
        static Dictionary<InstaUserActionType, float> ActionRation = new Dictionary<InstaUserActionType, float>
        {
            { InstaUserActionType.LikeByLocation, 0.4f },
            { InstaUserActionType.LikeByTag, 0.4f },
            { InstaUserActionType.Sunscribe, 0.2f }
        };


        IEnumerable<InstaAction> _executedActions;

        IEnumerable<InstaUserActionType> _actions;

        string _login;
        string _password;

        ICacheProvider _cacheProvider;

        string[] _tags;

        public ActionStrategy(string login, string password,
            string[] tags,
            IEnumerable<InstaUserActionType> actions,
            IEnumerable<InstaAction> executedActions,
            ICacheProvider cacheProvider)
        {
            _actions = actions;
            _login = login;
            _password = password;
            _cacheProvider = cacheProvider;
            _tags = tags;
            _executedActions = executedActions;
        }


        IEnumerable<InstaAction> GetActionsForLastHour()
        {
            var from = DateTime.UtcNow.AddHours(-1);
            return _executedActions.Where(a => a.ExecutedAt > from);
        }

        int GetAvailableActionCount()
        {
            var lastHourActions = GetActionsForLastHour();
            var availableActionCount = TotalActionsPerHour - lastHourActions.Count();

            if (availableActionCount <= 0)
            {
                return 0;
            }

            if (availableActionCount > MaxActionPerTime)
            {
                return _random.Next(1, MaxActionPerTime);
            }

            return availableActionCount;
        }

        //todo: сделать пропорцию
        public Task<IEnumerable<InstaAction>> Execute()
        {
            var availableActionCount = GetAvailableActionCount();
            if (availableActionCount == 0)
            {
                return Task.FromResult<IEnumerable<InstaAction>>(null);
            }

            /*
            var realActionRatio = _executedActions
                .GroupBy(a => a.Type)
                .ToDictionary(g => g.Key, g.Count());
                */

            InstaUserActionType[] types = new InstaUserActionType[availableActionCount];

            for (int i = 0; i < availableActionCount; i++)
            {
                types[i] = InstaUserActionType.LikeByTag;
            }

            return ExecuteAction(types);
        }

        string GetTag()
        {
            return _tags[_random.Next(_tags.Length)];
        }

        async Task<IEnumerable<InstaAction>> ExecuteAction(InstaUserActionType[] types)
        {
            var result = new LinkedList<InstaAction>();

            var api = new InstaApiBuilder()
                .UseLogger(new Logger())
                .SetUser(new UserSessionData { UserName = _login, Password = _password })
                .Build();

            IResult<bool> loggedIn = await api.LoginAsync();

            var currentUser = await api.GetCurrentUserAsync();

            for (int i = 0; i < types.Length; i++)
            {
                var startedAt = DateTime.UtcNow;
                var action = await ExecuteLikeByTag(api, currentUser.Value.Pk);
                if (action != null)
                {
                    result.AddLast(action);
                }
                TimeSpan actionTime = DateTime.UtcNow - startedAt;
                if (actionTime < MinTimeBetweenActions) //ждём, что бы между действиями был перерыв
                {
                    //MinTimeBetweenActions - actionTime;
                    await Task.Delay(MinTimeBetweenActions - actionTime);
                }
            }

            return result;
        }

        async Task<LikeByTagAction> ExecuteLikeByTag(IInstaApi api, string userPk, int maxPages = 1)
        {
            var tag = GetTag();
            var key = $"{_login}_{tag}";

            var duration = TimeSpan.FromMinutes(5);
            var medias = await _cacheProvider.AddOrGetExistingAsync(key, async () =>
            {
                IResult<InstaFeed> res = await api.GetTagFeedAsync(tag, maxPages);
                if (!res.Value.Medias.Any())
                {
                    return null;
                }
                return res.Value.Medias.Where(m => !m.Likers.Any(l => l.Pk == userPk)).ToArray();
            }, duration);

            if (medias == null)
            {
                return null;
            }


            var media = medias.FirstOrDefault();

            if (media == null)
            {
                _cacheProvider.Remove(key);
                return await ExecuteLikeByTag(api, userPk, maxPages + 5);
            }

           var likeResult = await api.LikeMediaAsync(media.Pk);
        
            if(!likeResult.Succeeded)
            {
                return null;
            }

            _cacheProvider.Update(key, Task.FromResult(medias.Where(m => m.Pk != media.Pk).ToArray()), duration);

            return new LikeByTagAction(media.Pk, DateTime.UtcNow);
        }
    }
}
