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

namespace ImageGallery.DatabaseRequests.Pictures
{
    public class GetPicturesByRoom : IRequest<IEnumerable<PictureDTO>>
    {
        public Guid RoomId { get; set; }
        public class GetPicturesByRoomHandler : IRequestHandler<GetPicturesByRoom, IEnumerable<PictureDTO>>
        {
            private readonly IImageGalleryContext _context;
            private readonly IMapper _mapper;

            public GetPicturesByRoomHandler(IImageGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<PictureDTO>> Handle(GetPicturesByRoom getPicturesByRoom, CancellationToken cancellationToken)
            {
                var pictures = await _context.Pictures.AsNoTracking()
                    .Where(p => p.RoomId == getPicturesByRoom.RoomId)
                    .OrderBy(p => p.CreateDate)
                    .ProjectTo<PictureDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return pictures.AsReadOnly();
            }
        }
    }
}
