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

namespace ImageGallery.DatabaseRequests.Pictures
{
    public class GetPicturesByRoom : IRequest<IQueryable<PictureDTO>>
    {
        public Guid RoomId { get; set; }
        public class GetPicturesByRoomHandler : IRequestHandler<GetPicturesByRoom, IQueryable<PictureDTO>>
        {
            private readonly IImageGalleryContext _context;
            private readonly IMapper _mapper;

            public GetPicturesByRoomHandler(IImageGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IQueryable<PictureDTO>> Handle(GetPicturesByRoom getPicturesByRoom, CancellationToken cancellationToken)
            {
                IQueryable<PictureDTO> pictures;

                if (getPicturesByRoom.RoomId == Guid.Empty)
                {
                    pictures = _context.Pictures.AsNoTracking()
                            .Where(p => !p.RoomId.HasValue)
                            .OrderBy(p => p.CreateDate)
                            .ProjectTo<PictureDTO>(_mapper.ConfigurationProvider);
                }
                else
                {
                    pictures = _context.Pictures.AsNoTracking()
                            .Where(p => p.RoomId == getPicturesByRoom.RoomId)
                            .OrderBy(p => p.CreateDate)
                            .ProjectTo<PictureDTO>(_mapper.ConfigurationProvider);
                }

                return await Task.FromResult(pictures);
            }
        }
    }
}
