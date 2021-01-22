using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageGallery.Exceptions
{
    public class PictureNotFoundException : AppBaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status404NotFound;

        public PictureNotFoundException() : base() { }
        public PictureNotFoundException(string message) : base(message) { }
        public PictureNotFoundException(string message, Exception inner) : base(message, inner) { }

        public PictureNotFoundException(Guid id) : base($"Picture with id '{id}' not found.") { }
    }
}
