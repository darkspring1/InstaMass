﻿using System.Threading.Tasks;

namespace SM.Domain.Events
{
    public interface ICanHandle<T> where T : IDomainEvent
    {
        void Handle(T args);

        Task HandleAsync(T args);
    }
}
