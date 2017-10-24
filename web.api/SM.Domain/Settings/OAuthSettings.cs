namespace SM.Domain.Settings
{
    public class OAuthSettings
    {
        static Domain.Settings.OAuthProviderSettings _fb;
        static Domain.Settings.OAuthProviderSettings _vk;
        public Domain.Settings.OAuthProviderSettings FB
        {
            get
            {
                if (_fb == null)
                {
                    _fb = new Domain.Settings.FBOAuthSettings();
                }
                return _fb;
            }
        }

        public Domain.Settings.OAuthProviderSettings VK
        {
            get
            {
                if (_vk == null)
                {
                    _vk = new Domain.Settings.VKOAuthSettings();
                }
                return _vk;
            }
        }
    }
}
