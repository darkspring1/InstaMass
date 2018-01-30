using System;

namespace SM.WEB.Application.DTO
{
    /// <summary>
    /// Пара токенов, refresh и access
    /// </summary>
    public class TokenData
    {
        public TokenData(string accessToken, string refreshToken, DateTime accessTokenExpiresAt)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            AccessTokenExpiresAt = accessTokenExpiresAt;
        }

        public string AccessToken { get; }
        public string RefreshToken { get; }
        public DateTime AccessTokenExpiresAt { get; }
    }
}
