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
    public class GetAllPictures : IRequest<IEnumerable<PictureDTO>>
    {
        public class GetAllPicturesHandler : IRequestHandler<GetAllPictures, IEnumerable<PictureDTO>>
        {
            private readonly IImageGalleryContext _context;
            private readonly IMapper _mapper;

            public GetAllPicturesHandler(IImageGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<PictureDTO>> Handle(GetAllPictures getAllPictures, CancellationToken cancellationToken)
            {
                var pictures = await _context.Pictures.AsNoTracking()
                    .OrderBy(p => p.CreateDate)
                    .ProjectTo<PictureDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return pictures.AsReadOnly();
            }
        }
    }
}
