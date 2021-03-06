﻿using System;
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

namespace ImageGallery.Features.Pictures
{
    public class GetPicture : IRequest<PictureWithDataDTO>
    {
        public Guid Id { get; set; }

        public class GetPictureHandler : IRequestHandler<GetPicture, PictureWithDataDTO>
        {
            private readonly IImageGalleryContext _context;
            private readonly IMapper _mapper;

            public GetPictureHandler(IImageGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PictureWithDataDTO> Handle(GetPicture getPicture, CancellationToken cancellationToken)
            {
                var pictureDto = await _context.Pictures.AsNoTracking()
                     .ProjectTo<PictureWithDataDTO>(_mapper.ConfigurationProvider)
                     .FirstOrDefaultAsync(a => a.Id == getPicture.Id, cancellationToken: cancellationToken);

                if (pictureDto == null)
                   throw new PictureNotFoundException(getPicture.Id);

                return pictureDto;
            }
        }
    }
}
