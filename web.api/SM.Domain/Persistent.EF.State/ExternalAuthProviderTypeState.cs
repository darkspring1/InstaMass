using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Persistent.EF.State
{
    public class ExternalAuthProviderTypeState
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }
    }
}
