using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Model;
using SM.Domain.Persistent;
using System.Threading.Tasks;
using SM.Domain.Events;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SM.WEB.Application.DTO;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace SM.WEB.Application.Services
{
    public class UserService : ApplicationService
    {
        public UserService(IUnitOfWork unitOfWork, ILogger logger, IDomainEventDispatcher eventDispatcher) : base(unitOfWork, logger, eventDispatcher)
        {
        }

        string GuidToStr(Guid guid)
        {
            return guid.ToString("N");
        }

        TokenData GetTokenData(
            string userId,
            double accessTokenLifeTime,
            double refreshTokenLifeTime,
            string issuer,
            string audience,
            SymmetricSecurityKey key)
        {
            var accessClaims = new Claim[] {
                    new Claim(Claims.UserId, userId),
                    new Claim(Claims.Access, "")
                };

            var refreshClaims = new Claim[] {
                    new Claim(Claims.UserId, userId),
                    new Claim(Claims.Refresh, "")
                };
            var now = DateTime.UtcNow;
            var algorithm = SecurityAlgorithms.HmacSha256;

            var accessTokenExpiresAt = now.AddSeconds(accessTokenLifeTime);
            var refreshTokenExpiresAt = now.AddSeconds(refreshTokenLifeTime);
            // создаем JWT-токен
            var accessToken = new JwtSecurityToken(
                    claims: accessClaims,
                    expires: accessTokenExpiresAt,
                    issuer: issuer,
                    audience: audience,
                    signingCredentials: new SigningCredentials(key, algorithm));

            var refreshToken = new JwtSecurityToken(
                    claims: refreshClaims,
                    expires: refreshTokenExpiresAt,
                    issuer: issuer,
                    audience: audience,
                    signingCredentials: new SigningCredentials(key, algorithm));

            var accessTokenWriten = new JwtSecurityTokenHandler().WriteToken(accessToken);
            var refreshTokenWriten = new JwtSecurityTokenHandler().WriteToken(refreshToken);

            UnitOfWork.AuthTokenRepository.AddNewToken(AuthToken.Create(refreshTokenWriten, userId, refreshTokenExpiresAt));

            return new TokenData(accessTokenWriten, refreshTokenWriten, accessTokenExpiresAt);
        }


        /// <summary>
        /// создаст нового пользователя и токен для него
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="accessTokenLifeTime"></param>
        /// <param name="refreshTokenLifeTime"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<ServiceResult<TokenData>> CreateNewUserAync(
            string email,
            string userName,
            string password,
            double accessTokenLifeTime,
            double refreshTokenLifeTime,
            string issuer,
            string audience,
            SymmetricSecurityKey key)
        {
            return RunAsync(async () => {
                var newUser = User.Create(email, userName, password);
                UnitOfWork.UserRepository.RegisterNewUser(newUser);
                var result = GetTokenData(GuidToStr(newUser.Id), accessTokenLifeTime, refreshTokenLifeTime, issuer, audience, key);
                await UnitOfWork.CompleteAsync();
                return result;
            });  
        }


        Task<AuthToken[]> GetOldTokensAsync(string userId)
        {
            return UnitOfWork.AuthTokenRepository.GetBySubjectAsync(userId);
        }

        /// <summary>
        /// получить новую пару токенов
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldToken"></param>
        /// <param name="accessTokenLifeTime"></param>
        /// <param name="refreshTokenLifeTime"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<ServiceResult<TokenData>> RefreshTokenAync(
            Guid userId,
            string oldToken,
            double accessTokenLifeTime,
            double refreshTokenLifeTime,
            string issuer,
            string audience,
            SymmetricSecurityKey key)
        {
            return RunAsync(async () =>
            {
                var userIdStr = GuidToStr(userId);
                var oldTokens = await GetOldTokensAsync(userIdStr);
                if (oldTokens.Any(t => t.Token == oldToken))
                {
                    UnitOfWork.AuthTokenRepository.Remove(oldTokens);
                }
                var result = GetTokenData(userIdStr, accessTokenLifeTime, refreshTokenLifeTime, issuer, audience, key);
                await UnitOfWork.CompleteAsync();
                return result;
            });
        }

        public Task<ServiceResult<TokenData>> FindByEmailAsync(
            string email,
            double accessTokenLifeTime,
            double refreshTokenLifeTime,
            string issuer,
            string audience,
            SymmetricSecurityKey key)
        {
            return RunAsync(async () =>
            {
                var user = await UnitOfWork.UserRepository.FindAsync(email);
                var userIdStr = GuidToStr(user.Id);
                var oldTokens = await GetOldTokensAsync(userIdStr);
                UnitOfWork.AuthTokenRepository.Remove(oldTokens);
                var result = GetTokenData(userIdStr, accessTokenLifeTime, refreshTokenLifeTime, issuer, audience, key);
                await UnitOfWork.CompleteAsync();
                return result;
            });
        }

        public Task<ServiceResult<User>> FindAsync(string email, string password)
        {
            return RunAsync(() => UnitOfWork.UserRepository.FindAsync(email, password));
        }
    }
}
