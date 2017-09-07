using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Domain.Persistent.EF.State
{

    public class AccountState
    {
        [Required]
        [MaxLength(1024)]
        public string Password { get; set; }

        [Key, Column(Order = 0)]
        [Required]
        [MaxLength(100)]
        public string Login { get; set; }
       
        public DateTime CreatedAt { get; set; }

        [Key, Column(Order = 1)]
        public Guid UserId { get; set; }
    }
}
