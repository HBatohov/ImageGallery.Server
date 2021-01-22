using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

using ImageGallery.Data;
using ImageGallery.Models.DTO;

namespace ImageGallery.DatabaseRequests.Rooms
{
    public class GetAllRooms : IRequest<IQueryable<RoomDTO>>
    {
        public class GetAllRoomsHandler : IRequestHandler<GetAllRooms, IQueryable<RoomDTO>>
        {
            private readonly IImageGalleryContext _context;
            private readonly IMapper _mapper;

            public GetAllRoomsHandler(IImageGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IQueryable<RoomDTO>> Handle(GetAllRooms getAllRooms, CancellationToken cancellationToken)
            {
                IQueryable<RoomDTO> rooms = _context.Rooms.AsNoTracking()
                    .OrderBy(p => p.Name)
                    .ProjectTo<RoomDTO>(_mapper.ConfigurationProvider);
                return await Task.FromResult(rooms);
            }
        }
    }
}
