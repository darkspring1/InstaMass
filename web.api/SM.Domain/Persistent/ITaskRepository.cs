using SM.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SM.Domain.Persistent
{
    public interface ITaskRepository
    {
        Task<SMTask[]> GetByUserAsync(Guid userId);
    }
}
