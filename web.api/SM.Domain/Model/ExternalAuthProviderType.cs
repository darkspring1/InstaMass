using System;

namespace SM.Domain.Model
{
    public enum ExternalAuthProviderType {
        Facebook = 0
    }


    public static class ExternalAuthProviderTypeExtension
    {
        public static ExternalAuthProviderType ToExternalAuthProviderType(this string value)
        {
            return (ExternalAuthProviderType)Enum.Parse(typeof(ExternalAuthProviderType), value, true);
        }
    }
}
