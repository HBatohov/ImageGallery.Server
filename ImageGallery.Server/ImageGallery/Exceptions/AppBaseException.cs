using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageGallery.Exceptions
{
    public class AppBaseException : Exception
    {
        public virtual int ErrorCode { get; } = StatusCodes.Status500InternalServerError;
        
        public AppBaseException() : base() { }
        public AppBaseException(string message) : base(message) { }
        public AppBaseException(string message, Exception inner) : base(message, inner) { }
    }
}
