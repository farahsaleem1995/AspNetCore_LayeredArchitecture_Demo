using LayeredArch.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Infra.Data.Helpers
{
    public class LogInResult : ILogInResult
    {
        public bool Succeeded { get; private set; }
        public bool IsLockedOut { get; private set; }
        public bool IsNotAllowed { get; private set; }

        public LogInResult(bool succeeded, bool isLockedOut, bool isNotAllowed)
        {
            this.Succeeded = succeeded;
            this.IsLockedOut = isLockedOut;
            this.IsNotAllowed = isNotAllowed;
        }
    }
}
