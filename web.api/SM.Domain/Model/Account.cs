using SM.Domain.Persistent.EF.State;
using System;

namespace SM.Domain.Model
{
    public class Account : Entity<AccountState>
    {

        internal Account(AccountState state) : base(state)
        {

        }

        public static Account Create(Guid userId, string instagramLogin, string instagramPassword)
        {
            return new Account(new AccountState { CreatedAt = DateTime.UtcNow, UserId = userId, Login = instagramLogin, Password = instagramLogin });
        }

        public Guid Id => State.Id;
         
        public string Login => State.Login;
    }
}
