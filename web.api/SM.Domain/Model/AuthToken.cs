using System;

namespace SM.Domain.Model
{
    public class AuthToken
    {
        internal AuthToken()
        {
        }

        public static AuthToken Create(string token, string subject, DateTime? expiresAt)
        {
            return new AuthToken
            {
                Token = token,
                Subject = subject,
                ExpiresAt = expiresAt
            };
        }

        public string Token { get; private set; }
        internal string Subject { get; set; }
        DateTime? ExpiresAt { get; set; }
    }
}
