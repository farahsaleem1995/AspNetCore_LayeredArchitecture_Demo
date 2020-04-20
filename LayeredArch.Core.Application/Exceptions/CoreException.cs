using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Application.Exceptions
{
    public class CoreException : Exception
    {
        public int StatusCode { get; set; }

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
        }
    }
}
