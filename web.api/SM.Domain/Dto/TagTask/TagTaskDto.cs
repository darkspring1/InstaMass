using System;

namespace SM.Domain.Dto.TagTask
{
    public class TagTaskDto
    {
        public string[] Tags { get; set; }

        public Guid AccountId { get; set; }

        public bool AvatarExistDisabled { get; set; }

        public SwitchedPropertyDto LastPost { get; set; }

        public SwitchedRangeDto Posts { get; set; }
        public SwitchedRangeDto Followers { get; set; }
        public SwitchedRangeDto Followings { get; set; }

    }
}
