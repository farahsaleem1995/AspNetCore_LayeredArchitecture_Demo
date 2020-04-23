using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Domain.Interfaces
{
    public interface IUserResult
    {
        bool Succeeded { get; set; }
        string Error { get; set; }
    }
}
