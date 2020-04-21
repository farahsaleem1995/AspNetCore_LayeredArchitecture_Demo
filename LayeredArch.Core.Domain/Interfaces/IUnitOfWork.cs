using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Core.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
