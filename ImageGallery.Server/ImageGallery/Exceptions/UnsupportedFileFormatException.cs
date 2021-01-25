using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageGallery.Exceptions
{
    public class UnsupportedFileFormatException : AppBaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status415UnsupportedMediaType;

        public UnsupportedFileFormatException() : base() { }
        public UnsupportedFileFormatException(string fileName) : base($"File '{fileName}' contains an unsupported data format.") { }
    }
}
