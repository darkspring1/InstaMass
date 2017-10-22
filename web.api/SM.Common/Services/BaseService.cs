using SM.Common.Log;
using System;
using System.Threading.Tasks;

namespace SM.Common.Services
{
    public abstract class BaseService
    {

        protected Log.ILogger Logger { get; }

        public BaseService(ILogger logger)
        {
            Logger = logger;
        }

        
        protected ServiceResult<T> Run<T>(Func<T> action)
        {
            try
            {
                return ServiceResult<T>.Success(action());
            }

            catch (Exception e)
            {
                Logger.Error(e);
                return ServiceResult<T>.Error(e);
            }
        }

        protected ServiceResult Run(Action action)
        {
            try
            {
                action();
                return ServiceResult.Success();
            }

            catch (Exception e)
            {
                Logger.Error(e);
                return ServiceResult.Error(e);
            }
        }

        protected async Task<ServiceResult<T>> RunAsync<T>(Func<Task<T>> action)
        {
            try
            {
                return ServiceResult<T>.Success(await action());
            }

            catch (Exception e)
            {
                Logger.Error(e);
                return ServiceResult<T>.Error(e);
            }
        }


        protected async Task<ServiceResult> RunAsync(Func<Task> action)
        {
            try
            {
                await action();
                return ServiceResult.Success();
            }

            catch (Exception e)
            {
                Logger.Error(e);
                return ServiceResult.Error(e);
            }
        }

    }
}