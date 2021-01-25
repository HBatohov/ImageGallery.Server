using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using ImageGallery.DatabaseRequests.UserAccounts;

namespace ImageGallery.Controllers
{
    [ApiController]
    [Route("/api/accounts")]
    public class AccountController : ControllerBase
    {
        private IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create(CreateUserAccount createUserAccount)
        {
            var result = await _mediator.Send(createUserAccount);
            return Ok(result);
        }

    }
}
