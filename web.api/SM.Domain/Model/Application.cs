using System;
using ApplicationState = SM.Domain.Persistent.EF.State.ApplicationState;

public enum ApplicationTypes
{
    JavaScript = 0,
    NativeConfidential = 1
};

namespace SM.Domain.Model
{
    public class Application : Entity<ApplicationState>
    {

        internal Application(ApplicationState state) : base(state)
        {
            Id = state.Id;
            Secret = state.Secret;
            Type = (ApplicationTypes)Enum.Parse(typeof(ApplicationTypes), state.Type, true);
            RefreshTokenLifeTime = state.RefreshTokenLifeTime;
            AllowedOrigin = state.AllowedOrigin;
        }

        public string Id { get; }
        public string Secret { get; }
        public ApplicationTypes Type { get; }
        public bool Active { get; }
        public int RefreshTokenLifeTime { get; }
        public string AllowedOrigin { get; }
    }
}
