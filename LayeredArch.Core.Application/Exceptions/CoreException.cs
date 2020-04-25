using LayeredArch.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Application.Exceptions
{
    public class CoreException : Exception, ICoreException
    {
        public int StatusCode { get; private set; }

        public string ErrorMessgae { get; private set; }

        public CoreException()
        {
        }

        public CoreException(string message)
            : base(message)
        {
        }

        public CoreException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public CoreException(int statusCode, string message)
            : base(message)
        {
            this.StatusCode = statusCode;
            this.ErrorMessgae = message;
        }
    }
}
