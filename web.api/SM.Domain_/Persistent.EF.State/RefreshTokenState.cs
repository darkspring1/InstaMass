using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Persistent.EF.State
{
    public class RefreshTokenState
    {
        [Key]
        [MaxLength(4096)]
        public string Id { get; set; }
   
        [Required]
        [MaxLength(50)]
        public string ApplicationId { get; set; }
        
        [Required]
        public string ProtectedTicket { get; set; }
    }
}
