using System;
using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Persistent.EF.State
{

    public class AccountState
    {

        public Guid Id { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Password { get; set; }

        
        [Required]
        [MaxLength(100)]
        public string Login { get; set; }
       
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
    }
}
