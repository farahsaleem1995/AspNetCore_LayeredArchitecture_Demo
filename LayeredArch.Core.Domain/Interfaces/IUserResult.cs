using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Domain.Interfaces
{
    public interface IUserResult
    {
        bool Succeeded { get; }
        IEnumerable<string > Errors { get;  }
    }
}
