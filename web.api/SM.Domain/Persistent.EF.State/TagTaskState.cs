using System;
using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Persistent.EF.State
{
    public class TagTaskState
    {
        [Key]
        public Guid TaskId { get; set; }
        public TaskState Task { get; set; }
        public string Tags { get; set; }
    }
}
