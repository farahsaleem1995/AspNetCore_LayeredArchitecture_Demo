using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Domain.Interfaces
{
    public interface ILogInResult
    {
        bool Succeeded { get; set; }
        bool IsLockedOut { get; set; }
        bool IsNotAllowed { get; set; }
    }
}
