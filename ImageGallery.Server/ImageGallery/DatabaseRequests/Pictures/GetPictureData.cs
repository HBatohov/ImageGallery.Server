using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

using ImageGallery.Models.DTO;
using ImageGallery.Data;
using ImageGallery.Exceptions;

namespace ImageGallery.DatabaseRequests.Pictures
{
    public class GetPictureData : IRequest<PictureDataDTO>
    {
        public Guid Id { get; set; }

        public class GetPictureDataHandler : IRequestHandler<GetPictureData, PictureDataDTO>
        {
            private readonly IImageGalleryContext _context;
            private readonly IMapper _mapper;

            public GetPictureDataHandler(IImageGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PictureDataDTO> Handle(GetPictureData getPicture, CancellationToken cancellationToken)
            {
                var pictureDto = await _context.Pictures.AsNoTracking()
                     .ProjectTo<PictureDataDTO>(_mapper.ConfigurationProvider)
                     .FirstOrDefaultAsync(a => a.Id == getPicture.Id, cancellationToken: cancellationToken);

                if (pictureDto == null)
                   throw new PictureNotFoundException(getPicture.Id);

                return pictureDto;
            }
        }
    }
}
