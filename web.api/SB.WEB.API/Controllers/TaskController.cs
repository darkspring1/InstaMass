using SM.Common.Log;
using SM.Domain.Model;
using SM.WEB.API.Models;
using SM.WEB.Application.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace SM.WEB.API.Controllers
{
    [Authorize]
    public class TaskController : BaseController
    {
        private readonly Func<TaskService> _taskServiceServiceFunc;

        public TaskController(ILogger logger, Func<TaskService> taskServiceServiceFunc) : base(logger)
        {
            _taskServiceServiceFunc = taskServiceServiceFunc;
        }

        [HttpGet]
        [Route(Routes.ApiTasks)]
        public Task<IHttpActionResult> GetTasks()
        {
            return ActionResultAsync(_taskServiceServiceFunc().GetTasks(UserId));
        }


        [HttpPost]
        [Route(Routes.ApiTaskTag)]
        public Task<IHttpActionResult> CreateTagTask(NewTagTaskModel model)
        {
            var lastPost = new SwitchedProperty(model.LastPost.Value, model.LastPost.Disabled);
            var posts = FromModel(model.Posts);
            var followers = FromModel(model.Followers);
            var followings = FromModel(model.Followings);
            return ActionResultAsync(_taskServiceServiceFunc().CreateTagTask(model.AccountId, model.Tags, model.AvatarExistDisabled, lastPost, posts: posts, followers: followers, followings: followings));
        }


        SwitchedRange FromModel(SwitchedRangeModel model) {
            return new SwitchedRange(model.From, model.To, model.Disabled);
        }


    }
    
}
