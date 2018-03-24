﻿using SM.Domain.Events;
using SM.Domain.Events.Sync;
using SM.Domain.Model;
using SM.Domain.Persistent;
using SM.Domain.Specification;
using System;
using System.Threading.Tasks;

namespace SM.WEB.Application.DomainEventHandlers
{
    public class TagTaskSyncHandler : ICanHandle<TagTaskWasSyncWithExternalSystem>
    {
        IUnitOfWork _unitOfWork;
        public TagTaskSyncHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Handle(TagTaskWasSyncWithExternalSystem args)
        {
            
        }

        public async Task HandleAsync(TagTaskWasSyncWithExternalSystem domainEvent)
        {
            TagTask task = await _unitOfWork.TagTaskRepository.FirstAsync(TagTaskSpecifications.GetById(Guid.Parse(domainEvent.TaskId)));
            task.InceraseExternalSystemVersion(domainEvent.ExternalSystemVersion);
            await _unitOfWork.CompleteAsync();
        }
    }
}
