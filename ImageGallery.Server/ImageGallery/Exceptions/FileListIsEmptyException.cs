using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageGallery.Exceptions
{
    public class FileListIsEmptyException : AppBaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status400BadRequest;

        public FileListIsEmptyException() : base() { }
        public FileListIsEmptyException(string message) : base(message) { }
        public FileListIsEmptyException(string message, Exception inner) : base(message, inner) { }
    }
}
