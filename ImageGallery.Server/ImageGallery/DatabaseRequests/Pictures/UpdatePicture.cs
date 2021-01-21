using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ImageGallery.Models.DTO;
using Microsoft.EntityFrameworkCore;
using ImageGallery.Data;

namespace ImageGallery.DatabaseRequests.Pictures
{
    public class UpdatePicture : IRequest<Guid>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? AlbumId { get; set; }

        public class UpdatePictureHandler : IRequestHandler<UpdatePicture, Guid>
        {
            private readonly IImageGalleryContext _context;
            private readonly IMapper _mapper;

            public UpdatePictureHandler(IImageGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(UpdatePicture updatePicture, CancellationToken cancellationToken)
            {
                var picture = _context.Pictures.FirstOrDefault(a => a.Id == updatePicture.Id);

                if (picture == null)
                {
                    throw new Exception("Picture Not Found.");
                    // HB -    throw new PictureNotFoundException(updatePicture.Id);
                }
                else
                {
                    // HB - validation 'Name'
                    _mapper.Map(updatePicture, picture);
                    await _context.SaveChangesAsync(cancellationToken);
                    return picture.Id;
                }
            }
        }
    }
}
