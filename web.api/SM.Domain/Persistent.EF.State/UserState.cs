using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Persistent.EF.State
{

    public class UserState
    {
        public UserState()
        {
            ExternalAuthProviders = new List<ExternalAuthProviderState>();
        }

        public Guid Id { get; set; }

        [MaxLength(1024)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
       

        public DateTime CreatedAt { get; set; }


        public IEnumerable<ExternalAuthProviderState> ExternalAuthProviders { get; set; }


    }
}
