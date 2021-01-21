using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ImageGallery.Models.DTO;
using ImageGallery.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ImageGallery.Data;

namespace ImageGallery.DatabaseRequests.Pictures
{
    public class AddPicture : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileMimeType { get; set; }
        public byte[] Data { get; set; }
        public Guid? RoomId { get; set; }
        public class AddPictureHandler : IRequestHandler<AddPicture, Guid>
        {
            private readonly IImageGalleryContext _context;
            private readonly IMapper _mapper;

            public AddPictureHandler(IImageGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(AddPicture addPicture, CancellationToken cancellationToken)
            {
                // HB - validation
                var picture = _mapper.Map<Picture>(addPicture);

                // HB - temporary
                picture.FileMimeType = "image/jpeg";
                picture.CreateDate = DateTime.Now;

                _context.Pictures.Add(picture);
                await _context.SaveChangesAsync(cancellationToken);
                return picture.Id;
            }
        }
    }
}
