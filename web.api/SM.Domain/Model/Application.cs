using System;
using ApplicationState = SM.Domain.Persistent.EF.State.ApplicationState;

namespace SM.Domain.Model
{
    public enum ApplicationTypes
    {
        JavaScript = 0,
        NativeConfidential = 1
    };


    public class Application : Entity<ApplicationState>
    {

        internal Application(ApplicationState state) : base(state)
        {
            Type = (ApplicationTypes)Enum.Parse(typeof(ApplicationTypes), state.Type, true);
        }

        public string Id => State.Id;
        public string Secret => State.Secret;
        public ApplicationTypes Type { get; }
        public bool Active => State.Active;
        public int RefreshTokenLifeTime => State.RefreshTokenLifeTime;
        public string AllowedOrigin => State.AllowedOrigin;
    }
}
