using System;
using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Persistent.EF.State
{
    public class RefreshTokenState
    {
        [Key]
        [MaxLength(1024)]
        [MinLength(1024)]
        public string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(50)]
        public string ApplicationId { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        [Required]
        [MinLength(4096)]
        public string ProtectedTicket { get; set; }
    }
}
