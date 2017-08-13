using System;

namespace SM.Domain.Persistent.EF.State
{

    public class UserState
    {
        public UserState()
        {
            
        }

        public Guid Id { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }
        
        
        public string UserName { get; set; }
       

        public DateTime CreatedAt { get; set; }



        
    }
}
