using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using ImageGallery.Data;
using ImageGallery.Exceptions;

namespace ImageGallery.Features.Pictures
{
    public class DeletePicture : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public class DeletePictureHandler : IRequestHandler<DeletePicture, Unit>
        {
            private readonly IImageGalleryContext _context;

            public DeletePictureHandler(IImageGalleryContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeletePicture deletePicture, CancellationToken cancellationToken)
            {
                var picture = await _context.Pictures.FirstOrDefaultAsync(a => a.Id == deletePicture.Id, cancellationToken: cancellationToken);

                if (picture == null)
                    throw new PictureNotFoundException(deletePicture.Id);

                _context.Pictures.Remove(picture);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
