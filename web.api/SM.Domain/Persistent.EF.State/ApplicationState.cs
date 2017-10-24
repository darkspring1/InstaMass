using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Persistent.EF.State
{
    public class ApplicationState
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(1024)]
        public string Secret { get; set; }
        [Required]
        [MaxLength(100)]
        public string Type { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        [MaxLength(100)]
        public string AllowedOrigin { get; set; }
    }
}
