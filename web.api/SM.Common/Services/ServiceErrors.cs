namespace SM.Common.Services
{
    public static class ServiceErrors
    {
        /// <summary>
        /// Can't like media
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static ServiceError Error1(string mediaId) => new ServiceError(1, $"Can't like media: {mediaId}");

        /// <summary>
        /// Account already registred
        /// </summary>
        /// <returns></returns>
        public static ServiceError Error2() => new ServiceError(2, "Account already registred");

        /// <summary>
        /// Can't load feed
        /// </summary>
        /// <returns></returns>
        public static ServiceError Error3() => new ServiceError(3, "Can't load feed");

        /// <summary>
        /// Like action max iteration
        /// </summary>
        /// <returns></returns>
        public static ServiceError Error4() => new ServiceError(4, "Like action max iteration");

        /// <summary>
        /// Requests limit
        /// </summary>
        /// <returns></returns>
        public static ServiceError Error5() => new ServiceError(5, "Requests limit");

        /// <summary>
        /// Instagram login error
        /// </summary>
        /// <returns></returns>
        public static ServiceError Error6() => new ServiceError(6, "Instagram login error");

    }
}
