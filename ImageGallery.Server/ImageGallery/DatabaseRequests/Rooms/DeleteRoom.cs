using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ImageGallery.Data;

namespace ImageGallery.DatabaseRequests.Rooms
{
    public class DeleteRoom : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoom, Unit>
        {
            private readonly IImageGalleryContext _context;

            public DeleteRoomCommandHandler(IImageGalleryContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteRoom command, CancellationToken cancellationToken)
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(a => a.Id == command.Id);

                if (room == null)
                    throw new Exception("Room Not Found.");
                // HB -    throw new RoomNotFoundException(command.Id);

                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
