using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace ImageGallery.Features.Rooms
{
    public class AddRoomValidator : AbstractValidator<AddRoom>
    {
        public AddRoomValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(260);
        }
    }
}
