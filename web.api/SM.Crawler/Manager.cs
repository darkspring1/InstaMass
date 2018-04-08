using InstaSharper.API;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Dto.TagTaskAction;
using SM.Domain.Model;
using SM.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SM.Crawler
{
    class Manager : BaseService
    {
        const int MaxIteration = 10;
        private readonly TagTaskService _tagTaskService;
        private readonly IInstaApiFactory _apiFactory;
        public Manager(TagTaskService tagTaskService, IInstaApiFactory apiFactory, ILogger logger) : base(logger)
        {
            _tagTaskService = tagTaskService;
            _apiFactory = apiFactory;
        }

        string GetRandomTag(string[] arr)
        {
            Random rand = new Random();
            var index = rand.Next(0, arr.Length);
            return arr[index];
        }

        public async Task StartAsync()
        {
            while (true)
            {
                var result = await _tagTaskService.GetTagTasksWithLastActionAsync();
                var dtos = await ExecuteTasksAsync(result.Result);
                await _tagTaskService.SaveActionsAsync(dtos);
            }

        }

        async Task<TagTaskActionDto[]> ExecuteTasksAsync(TagTask[] tagTasks)
        {
            var tasks = new List<Task<ServiceResult<TagTaskActionDto>>>();
            foreach (var t in tagTasks)
            {
                tasks.Add(ExecuteTaskAsync(t));
            }

            var serviceResults = await Task.WhenAll(tasks);

            return serviceResults.Select(sr => sr.Result).ToArray();
        }

        Task<ServiceResult<TagTaskActionDto>> ExecuteTaskAsync(TagTask tagTask)
        {
            return RunAsync(async () =>
            {
                var startedAt = DateTime.UtcNow;
                var api = _apiFactory.Create(tagTask.Account.Login, tagTask.Account.Password);
                var r = await api.LoginAsync();
                var currentUser = await api.GetCurrentUserAsync();
                var tag = GetRandomTag(tagTask.Tags);
                var serviceResult = await LikeAsync(api, tagTask, tag, currentUser.Value.Pk, 1, 1, MaxIteration);
                serviceResult.Result.IsSuccess = serviceResult.IsSuccess;
                serviceResult.Result.StartedAt = startedAt;
                serviceResult.Result.FinishedAt = DateTime.UtcNow;
                return serviceResult.Result;
            });

        }

        async Task<ServiceResult<TagTaskActionDto>> LikeAsync(
            IInstaApi api,
            TagTask tagTask,
            string tag,
            long userPk,
            int maxPagesToLoad,
            int curIteration,
            int maxIteration)
        {

            var dto = new TagTaskActionDto
            {
                Tag = tag,
                TaskId = tagTask.Id
            };

            if (curIteration > maxIteration)
            {
                return ServiceResult.Fault(ServiceErrors.Error4(), dto);
            }

            IResult<InstaFeed> res = await api.GetTagFeedAsync(tag, PaginationParameters.MaxPagesToLoad(maxPagesToLoad));

            if (!res.Succeeded)
            {
                return ServiceResult.Fault(ServiceErrors.Error3(), dto);
            }
            var media = res.Value
                .Medias
                .Where(m => !m.Likers.Any(l => l.Pk == userPk))
                .FirstOrDefault();

            if (media == null)
            {
                return await LikeAsync(api, tagTask, tag, userPk, maxPagesToLoad + 5, curIteration++, maxIteration);
            }

            dto.MediaPk = media.Pk;
            dto.MediaCode = media.Code;

            var likeResult = await api.LikeMediaAsync(media.Pk);

            if (!likeResult.Succeeded)
            {
                return ServiceResult.Fault(ServiceErrors.Error1(media.Pk), dto);
            }
            
            return ServiceResult.Success(dto);
        }


       
    }
}
