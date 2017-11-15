using System;

namespace SM.WEB.API.Models
{
    public class NewTagTaskModel
    {
        public string[] Tags { get; set; }

        public Guid AccountId { get; set; }
    }
}
