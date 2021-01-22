using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageGallery.Exceptions
{
    public class PipelineValidationException : AppBaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status400BadRequest;

        public PipelineValidationException() : base() { }
        public PipelineValidationException(string message) : base(message) { }
        public PipelineValidationException(string message, Exception inner) : base(message, inner) { }
    }
}
