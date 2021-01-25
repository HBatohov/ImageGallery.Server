using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MediatR;

using ImageGallery.Exceptions;
using ImageGallery.Models.Entities;

namespace ImageGallery.DatabaseRequests.UserAccounts
{
    public class CreateUserAccount : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class CreateAccountCommandHandler : IRequestHandler<CreateUserAccount, string>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            public CreateAccountCommandHandler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }
            public async Task<string> Handle(CreateUserAccount createUserAccount, CancellationToken cancellationToken)
            {
                var userByName = await _userManager.FindByNameAsync(createUserAccount.UserName);
                if (userByName != null)
                {
                    throw new UserAlreadyExistsException($"An account has already been registered for this User Name: '{createUserAccount.UserName}'.");
                }

                var userByEmail = await _userManager.FindByEmailAsync(createUserAccount.Email);
                if (userByEmail != null)
                {
                    throw new UserAlreadyExistsException($"An account has already been registered for this Email: '{createUserAccount.Email}'.");
                }

                var newUser = new ApplicationUser { UserName = createUserAccount.UserName, Email = createUserAccount.Email, 
                                                    FirsName = createUserAccount.FirstName, LastName = createUserAccount.LastName };
                var result = await _userManager.CreateAsync(newUser, createUserAccount.Password);
                if (result.Succeeded)
                    return newUser.Id;
                else
                    throw new CreateNewUserException(result.Errors);
            }
        } 
    }
}
