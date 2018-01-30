using System;

namespace SM.Domain.Persistent.EF.State
{
    public class AuthTokenState
    {
        public string Token { get; set; }
        public string Subject { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
