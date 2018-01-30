using SM.Domain.Persistent.EF.State;
using System;

namespace SM.Domain.Model
{
    public class AuthToken : Entity<AuthTokenState>
    {
        internal AuthToken(AuthTokenState state) : base(state)
        {
        }

        public string Token => State.Token;

        public static AuthToken Create(string token, string subject, DateTime? expiresAt)
        {
            var state = new AuthTokenState
            {
                Token = token,
                Subject = subject,
                ExpiresAt = expiresAt
            };
            return new AuthToken(state);
        }
    }
}
