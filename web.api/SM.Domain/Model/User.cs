using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using SM.Domain.Persistent.EF.State;
using System.Threading.Tasks;
using SM.Domain.Services;
using System.Collections.Generic;

namespace SM.Domain.Model
{
    public  class User : Entity<UserState>
    {
        public Guid Id => State.Id;


        internal User(UserState state) : base(state)
        {
            if (state.Email != null)
            {
                Email = new MailAddress(state.Email);
            }
        }

        public static User Create(string email, string userName, string password)
        {
            return new User(new UserState { CreatedAt = DateTime.UtcNow,  Email = email, UserName = userName, PasswordHash = SHA(password) });
        }

        public static User CreateExternal(ExternalUserInfo externalUserInfo)
        {
            ExternalAuthProviderTypeState externalAuthProviderType = new ExternalAuthProviderTypeState { Type = externalUserInfo.ProviderType.ToString() };
            ExternalAuthProviderState externalAuthProviderState = new ExternalAuthProviderState
            {
                ExternalUserId = externalUserInfo.UserId,
                ExternalAuthProviderType = externalAuthProviderType
            };

            var userState = new UserState
            {
                CreatedAt = DateTime.UtcNow,
                Email = externalUserInfo.Email,
                UserName = $"{externalUserInfo.FirstName} {externalUserInfo.LastName}",
                ExternalAuthProviders = new List<ExternalAuthProviderState> { externalAuthProviderState }
            };

            return new User(userState);
        }

        static ExternalAuthService GetExternalAuthService(ExternalAuthProviderType providerType)
        {
            return new FBAuthService();
        }

        public static Task<ExternalUserInfo> GetExternalUserInfoAsync(ExternalAuthProviderType providerType, string accessToken)
        {
            var s = GetExternalAuthService(providerType);
            return s.GetUserAsync(accessToken);
        }



        internal static string SHA(string str)
        {
            var crypt = new SHA512Managed();
            string hash = string.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(str), 0, Encoding.UTF8.GetByteCount(str));
            foreach (byte bit in crypto)
            {
                hash += bit.ToString("x2");
            }
            return hash;
        }

        
        public string UserName => State.UserName;


        public MailAddress Email { get; private set; }

        
    }
}
