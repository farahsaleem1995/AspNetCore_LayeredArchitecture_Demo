using LayeredArch.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayeredArch.Infra.Data.Helpers
{
    public class UserResult : IUserResult
    {
        public bool Succeeded { get; private set; }
        public IEnumerable<string > Errors { get; private set; }

        public UserResult(bool succeeded, IEnumerable<string> errors)
        {
            this.Succeeded = succeeded;
            this.Errors = errors;
        }
    }
}
