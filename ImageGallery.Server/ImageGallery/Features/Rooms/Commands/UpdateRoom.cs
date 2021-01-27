using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

using ImageGallery.Data;
using ImageGallery.Models.DTO;
using ImageGallery.Exceptions;

namespace ImageGallery.Features.Rooms
{
    public class UpdateRoom : IRequest<Guid>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public class UpdateRoomHandler : IRequestHandler<UpdateRoom, Guid>
        {
            private readonly IImageGalleryContext _context;
            private readonly IMapper _mapper;

            public UpdateRoomHandler(IImageGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(UpdateRoom updateRoom, CancellationToken cancellationToken)
            {
                var room = _context.Rooms.FirstOrDefault(a => a.Id == updateRoom.Id);

                if (room == null)
                {
                    throw new RoomNotFoundException(updateRoom.Id);
                }
                else
                {
                    _mapper.Map(updateRoom, room);
                    await _context.SaveChangesAsync(cancellationToken);
                    return room.Id;
                }
            }
        }
    }
}
