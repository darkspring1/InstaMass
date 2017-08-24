using SM.Domain.Persistent.EF.State;
using System;

namespace SM.Domain.Model
{
    public class RefreshToken : Entity<RefreshTokenState>
    {
       
        internal RefreshToken(RefreshTokenState state) : base(state)
        {
        }

        public static RefreshToken Create(string refreshTokenId, string subject, string applicationId, double refreshTokenLifeTime)
        {
            RefreshTokenState state = new RefreshTokenState
            {
                //store hash for security
                Id = GetHash(refreshTokenId),
                Subject = subject,
                ApplicationId = applicationId,
                IssuedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(refreshTokenLifeTime)
            };

            return new RefreshToken(state);
        }

        public static string GetHash(string refreshTokenId)
        {
            return User.SHA(refreshTokenId);
        }

        public string Id => State.Id;
        public string Subject => State.Subject;
        public string ApplicationId => State.ApplicationId;
        public DateTime IssuedAt => State.IssuedAt;
        public DateTime ExpiresAt => State.ExpiresAt;
        public string ProtectedTicket => State.ProtectedTicket;

        public void SetProtectedTicket(string value)
        {
            State.ProtectedTicket = value;
        }
    }
}
