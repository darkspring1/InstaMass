using System;

namespace SM.Common.Services
{

    public class ServiceResult
    {
        protected ServiceResult(bool isSuccess) {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }

        public bool IsFaulted => !IsSuccess;

        public Exception Exception { get; protected set; }

        public static ServiceResult Success()
        {
            return new ServiceResult(true);
        }

        public static ServiceResult Error(Exception e)
        {
            return new ServiceResult(false) { Exception = e };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        private ServiceResult(T result) : base(true)
        {
            Result = result;
        }

        private ServiceResult() : base(false) { }

        public T Result { get; }

        public static ServiceResult<T> Success(T result)
        {
            return new ServiceResult<T>(result);
        }

        public static new ServiceResult<T> Error(Exception e)
        {
            return new ServiceResult<T>() { Exception = e };
        }
    }
}
