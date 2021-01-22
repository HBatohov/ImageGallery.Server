using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageGallery.Exceptions
{
    public class RoomNotFoundException : AppBaseException
    {
        public override int ErrorCode { get; } = StatusCodes.Status404NotFound;
        
        public RoomNotFoundException() : base() { }
        public RoomNotFoundException(string message) : base(message) { }
        public RoomNotFoundException(string message, Exception inner) : base(message, inner) { }

        public RoomNotFoundException(Guid id) : base($"Room with id '{id}' not found.") { }
    }
}
