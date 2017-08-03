using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using System;
using System.Threading.Tasks;
using Xunit;

namespace InstaSharper.Tests.Infrastructure
{
    [Trait("Category", "Infrastructure")]
    public class ApiInstanceBuilderTest
    {
        [Fact]
        public async Task CreateApiInstanceWithBuilder()
        {
            var _login = "vasin5462";
            var _password = "supervasia";

            var result = new InstaApiBuilder()
                .UseLogger(new TestLogger())
                .SetUser(new UserSessionData { UserName = _login, Password = _password })
                .Build();

            try
            {
                var res = await result.LoginAsync();
                //var r = await result.GetTagFeedAsync("tree", 5);
                var r = await result.GetTagFeedAsync("titsgram", 10);
            }
            catch (Exception e)
            {

            }
            
            

            Assert.NotNull(result);
        }
    }
}