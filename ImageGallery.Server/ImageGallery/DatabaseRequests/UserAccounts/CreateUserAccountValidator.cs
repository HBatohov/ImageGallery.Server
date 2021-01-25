﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.DatabaseRequests.UserAccounts
{
    public class CreateUserAccountValidator : AbstractValidator<CreateUserAccount>
    {
        public CreateUserAccountValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(60);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(60);
        }
    }
}
