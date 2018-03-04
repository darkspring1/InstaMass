using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Model
{
    public  class User
    {
        public static User Create(string email, string userName, string password)
        {
            return new User
            {
                CreatedAt = DateTime.UtcNow,
                EmailStr = email,
                UserName = userName,
                PasswordHash = SHA(password)
            };
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

        public MailAddress Email => new MailAddress(EmailStr);

        internal User()
        {
            Id = Guid.NewGuid();
            ExternalAuthProviders = new List<ExternalAuthProvider>();
        }

        public Guid Id { get; private set; }

        [MaxLength(1024)]
        internal string PasswordHash { get; set; }

        [Required]
        [MaxLength(100)]
        internal string EmailStr { get; set; }

        [Required]
        [MaxLength(100)]
        internal string UserName { get; set; }

        private DateTime CreatedAt { get; set; }

        public ICollection<ExternalAuthProvider> ExternalAuthProviders { get; set; }
    }
}
