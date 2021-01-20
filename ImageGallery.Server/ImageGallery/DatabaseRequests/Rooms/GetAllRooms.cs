using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ImageGallery.Models.DTO;
using Microsoft.EntityFrameworkCore;
using ImageGallery.Data;

namespace ImageGallery.DatabaseRequests.Rooms
{
    public class GetAllRooms : IRequest<IEnumerable<RoomDTO>>
    {
        public class GetAllRoomsHandler : IRequestHandler<GetAllRooms, IEnumerable<RoomDTO>>
        {
            private readonly IImageGalleryContext _context;
            private readonly IMapper _mapper;

            public GetAllRoomsHandler(IImageGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<RoomDTO>> Handle(GetAllRooms request, CancellationToken cancellationToken)
            {
                var rooms = await _context.Rooms.AsNoTracking()
                    .OrderBy(p => p.Name)
                    .ProjectTo<RoomDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return rooms.AsReadOnly();
            }
        }
    }
}
