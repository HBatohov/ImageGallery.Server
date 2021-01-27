using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using MediatR;

using ImageGallery.Data;
using ImageGallery.Models.DTO;
using ImageGallery.Models.Entities;

namespace ImageGallery.Features.Pictures
{
    public class AddPicture : IRequest<IEnumerable<Guid>>
    {
        public IFormFileCollection FormFiles;
        public Guid? RoomId { get; set; }
        public class AddPictureHandler : IRequestHandler<AddPicture, IEnumerable<Guid>>
        {
            private readonly IImageGalleryContext _context;

            public AddPictureHandler(IImageGalleryContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Guid>> Handle(AddPicture addPicture, CancellationToken cancellationToken)
            {
                List<Picture> photos = new List<Picture>();

                foreach (var file in addPicture.FormFiles)
                {
                    using var ms = new MemoryStream();
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string fileMimeType = MimeTypes.GetMimeType(fileBytes, file.FileName);

                    var picture = new Picture
                    {
                        Name = file.FileName,
                        Data = fileBytes,
                        CreateDate = DateTime.Now,
                        FileMimeType = fileMimeType
                    };

                    if (addPicture.RoomId != Guid.Empty)
                        picture.RoomId = addPicture.RoomId;

                    photos.Add(picture);
                }

                if (photos.Count > 0)
                {
                    _context.Pictures.AddRange(photos);
                    await _context.SaveChangesAsync(cancellationToken);

                    return photos.Select(p => p.Id).ToList();
                }

                return null;
            }
        }
    }
}
