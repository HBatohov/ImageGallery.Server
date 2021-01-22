using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace ImageGallery.DatabaseRequests.Pictures
{
    public class UpdatePictureValidator : AbstractValidator<AddPicture>
    {
        public UpdatePictureValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(260);
            RuleFor(x => x.FileMimeType).NotEmpty().MaximumLength(260);

            // HB - check x.Data and allowed x.FileMimeType
        }
    }
}
