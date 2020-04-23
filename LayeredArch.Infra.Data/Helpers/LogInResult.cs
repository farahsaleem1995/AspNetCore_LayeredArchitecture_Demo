using LayeredArch.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Infra.Data.Helpers
{
    public class LogInResult : ILogInResult
    {
        public bool Succeeded { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
    }
}
