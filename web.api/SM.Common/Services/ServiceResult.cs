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

        public ServiceError Error { get; protected set; }

        public static ServiceResult Success()
        {
            return new ServiceResult(true);
        }

        public static ServiceResult<T> Success<T>(T result)
        {
            return new ServiceResult<T>(result);
        }

        public static ServiceResult Fault(Exception e)
        {
            return new ServiceResult(false) { Exception = e };
        }



        public static ServiceResult Fault(ServiceError error)
        {
            return new ServiceResult(false) { Error = error };
        }

        public static ServiceResult<T> Fault<T>(ServiceError error)
        {
            return new ServiceResult<T>() { Error = error };
        }

        public static ServiceResult<T> Fault<T>(ServiceError error, T result)
        {
            return new ServiceResult<T>() { Error = error, Result = result };
        }


        public static new ServiceResult<T> Fault<T>(Exception e)
        {
            return new ServiceResult<T>() { Exception = e };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        internal ServiceResult(T result) : base(true)
        {
            Result = result;
        }

        internal ServiceResult() : base(false) { }

        public T Result { get; internal set; }

        public bool IsFaultedOrNullResult => IsFaulted || Result == null;

        public bool IsSuccessAndNotNullResult => IsSuccess && Result != null;

        public ServiceResult<TDestination> CastToFault<TDestination>()
        {
            if (IsSuccess)
            {
                throw new Exception("ServiceResult should be faulted");
            }
            return new ServiceResult<TDestination>() {
                Exception = this.Exception,
                Error = this.Error
            };
        }


    }
}
