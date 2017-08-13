using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using SM.Domain.Persistent.EF.State;

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
            return new User(new UserState { Email = email, UserName = userName, PasswordHash = SHA(password) });
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
