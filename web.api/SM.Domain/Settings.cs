using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domain
{
    class Settings
    {
        public static string VkApiUrl => ConfigurationManager.AppSettings["vkApiUrl"];
        public static string VkApiVersion => ConfigurationManager.AppSettings["vkApiVersion"];
    }
}
