using SM.Domain.Persistent.EF.State;

namespace SM.Domain.Model
{
    public class RefreshToken : Entity<RefreshTokenState>
    {
       
        internal RefreshToken(RefreshTokenState state) : base(state)
        {
        }

        public static RefreshToken Create(string refreshTokenId, string applicationId, string protectedTicket)
        {
            RefreshTokenState state = new RefreshTokenState
            {
                //store hash for security
                Id = GetHash(refreshTokenId),
                ApplicationId = applicationId,
                ProtectedTicket = protectedTicket
            };

            return new RefreshToken(state);
        }

        public static string GetHash(string refreshTokenId)
        {
            return User.SHA(refreshTokenId);
        }

        public string ProtectedTicket => State.ProtectedTicket;
    }
}
