using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageGallery.Exceptions
{
    public class UserAlreadyExistsException : AppBaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status409Conflict;
        public UserAlreadyExistsException()
        {
        }

        public UserAlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}
