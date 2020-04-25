using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Application.Interfaces
{
    public interface ICoreException
    {
        public int StatusCode { get; }
        public string ErrorMessgae { get; }
    }
}
