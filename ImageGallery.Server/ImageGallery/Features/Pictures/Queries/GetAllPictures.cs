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

namespace ImageGallery.Features.Pictures
{
    public class GetAllPictures : IRequest<IQueryable<PictureDTO>>
    {
        public class GetAllPicturesHandler : IRequestHandler<GetAllPictures, IQueryable<PictureDTO>>
        {
            private readonly IImageGalleryContext _context;
            private readonly IMapper _mapper;

            public GetAllPicturesHandler(IImageGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IQueryable<PictureDTO>> Handle(GetAllPictures getAllPictures, CancellationToken cancellationToken)
            {
                IQueryable<PictureDTO> pictures = _context.Pictures.AsNoTracking()
                    .OrderBy(p => p.CreateDate)
                    .ProjectTo<PictureDTO>(_mapper.ConfigurationProvider);
                return await Task.FromResult(pictures);
            }
        }
    }
}
