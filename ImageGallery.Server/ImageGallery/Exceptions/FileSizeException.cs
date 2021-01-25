using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageGallery.Exceptions
{
    public class FileSizeException : AppBaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status400BadRequest;

        public FileSizeException() : base() { }
        public FileSizeException(string message) : base(message) { }
        public FileSizeException(string message, Exception inner) : base(message, inner) { }

        public FileSizeException(string fileName, long fileSize, long fileSizeLimit)
                : base($"The File '{fileName}' has size {fileSize} exceeds the maximum file size {fileSizeLimit}.") { }
    }
}
