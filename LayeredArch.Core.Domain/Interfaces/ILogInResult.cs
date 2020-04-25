using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Domain.Interfaces
{
    public interface ILogInResult
    {
        bool Succeeded { get; }
        bool IsLockedOut { get; }
        bool IsNotAllowed { get; }
    }
}
