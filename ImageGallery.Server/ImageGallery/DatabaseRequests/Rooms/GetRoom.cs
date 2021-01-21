using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ImageGallery.Models.DTO;
using ImageGallery.Data;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.DatabaseRequests.Rooms
{
    public class GetRoom : IRequest<RoomDTO>
    {
        public Guid Id { get; set; }

        public class GetRoomHandler : IRequestHandler<GetRoom, RoomDTO>
        {
            private readonly IImageGalleryContext _context;
            private readonly IMapper _mapper;

            public GetRoomHandler(IImageGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<RoomDTO> Handle(GetRoom getRoom, CancellationToken cancellationToken)
            {
                var roomDto = await _context.Rooms.AsNoTracking()
                     .ProjectTo<RoomDTO>(_mapper.ConfigurationProvider)
                     .FirstOrDefaultAsync(a => a.Id == getRoom.Id);

                if (roomDto == null)
                    throw new Exception("Room Not Found.");
                // HB -   throw new RoomNotFoundException(getRoom.Id);

                return roomDto;
            }
        }
    }
}
