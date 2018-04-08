namespace SM.Common.Services
{
    public class ServiceError
    {
        internal ServiceError(int code, string description)
        {
            Code = code;
            Description = description;
        }

        public int Code { get; }
        public string Description { get; }
    }
}
