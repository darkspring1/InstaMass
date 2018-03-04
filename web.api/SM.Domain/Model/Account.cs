using System;
using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Model
{
    public class Account
    {

        private Account()
        {

        }

        public static Account Create(Guid userId, string instagramLogin, string instagramPassword)
        {
            return new Account()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                Login = instagramLogin,
                Password = instagramLogin
            };
        }

        

        public Guid Id { get; private set; }

        [Required]
        [MaxLength(1024)]
        private string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string Login { get; internal set; }

        private DateTime CreatedAt { get; set; }

        internal Guid UserId { get; set; }

        public User User { get; set; }

    }
}
