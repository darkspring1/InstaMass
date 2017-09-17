using System;

namespace SM.WEB.API.Models
{
    public class NewLikeTaskModel
    {
        public string[] Tags { get; set; }

        public Guid AccountId { get; set; }
    }
}
