using System;
using System.Collections.Generic;
using System.Text;

namespace SM.TaskEngine.Api.ActorModel.Commands
{
    public class CreateTagTask
    {
        public string Id { get; set; }
        public string Login { get; set; }

        public string [] Tags { get; set; }
    }
}
