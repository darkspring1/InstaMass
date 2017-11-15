using System;

namespace SM.WEB.API.Models
{
    public class NewTagTaskModel
    {
        public string[] Tags { get; set; }

        public Guid AccountId { get; set; }

        public bool AvatarExist { get; set; }

        public SwitchedPropertyModel LastPost { get; set; }

        public SwitchedRangeModel Posts { get; set; }
        public SwitchedRangeModel Followers { get; set; }
        public SwitchedRangeModel Followings { get; set; }

    }
}
