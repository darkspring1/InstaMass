using SM.Domain.Persistent.EF.State;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Domain.Model
{
    public class ExternalAuthProvider
    {
        [Key, Column(Order = 0)]
        [Required]
        public string ExternalUserId { get; set; }

        [Key, Column(Order = 1)]
        public int ExternalAuthProviderTypeId { get; set; }

        public ExternalAuthProviderTypeState ExternalAuthProviderType { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
