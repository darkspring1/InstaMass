using Akka.Actor;
using Akka.Persistence;
using InstaMass.ActorModel.Commands;
using InstaMass.ActorModel.Events;
using InstaMfl.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InstaMass.Api.ActorModel.Actors
{
    public class UserActorState
    {
        public Guid Id { get; }
        public IEnumerable<InstaUser> InstaUsers { get; }

        public UserActorState(Guid id,  IEnumerable<InstaUser> instaUsers)
        {
            Id = id;
            InstaUsers = instaUsers;
        }
    }

    public class InstaUser
    {
        public string Login { get; }
        public string Password { get; }
        public IEnumerable<string> Tags { get; }

        public IEnumerable<InstaUserActionType> AllowedActions { get; }

        public InstaUser(string login, string password, IEnumerable<string> tags, IEnumerable<InstaUserActionType> allowedActions)
        {
            Login = login;
            Password = password;
            Tags = tags;
            AllowedActions = allowedActions;
        }
    }
}
