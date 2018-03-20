namespace SM.WEB.API
{
    class Routes
    {
        public const string ApiUser = "api/user";

        public const string Login = "Login";
        public const string Register = "Register";

        public const string LoginExternal = "LoginExternal";
        public const string RegisterExternal = "RegisterExternal";
        public const string TokenRefresh = "token/refresh";

        public const string ApiAccounts = "api/accounts";

        public const string ApiAccount = "api/account";

        public const string Tasks_Get = "tasks";

        public const string TagTask_Get = "tasks/tag/{id}";
        public const string TagTask_Post = "tasks/tag";
        public const string TagTask_Put = "tasks/tag/{id}";
    }
}

