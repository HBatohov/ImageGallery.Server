using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using LightQuery.Client;
using LightQuery.EntityFrameworkCore;

using ImageGallery.Constans;
using ImageGallery.Models.DTO;
using ImageGallery.DatabaseRequests.Pictures;

namespace ImageGallery.Controllers
{
    [Route("api/pictures")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PicturesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AsyncLightQuery(forcePagination: false, defaultPageSize: AppConstans.DEFAULT_PAGE_SIZE)]
        [ProducesResponseType(typeof(PaginationResult<PictureDTO>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAllPicturesAsync()
        {
            var result = await _mediator.Send(new GetAllPictures());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPictureAsync(Guid id)
        {
            var result = await _mediator.Send(new GetPicture() { Id = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePictureAsync(Guid id, UpdatePicture updatePicture)
        {
            updatePicture.Id = id;
            var result = await _mediator.Send(updatePicture);
            return Ok(result);
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> CreateAsync([FromForm] IFormFileCollection Files, [FromForm] Guid? RoomId)
        {
            var result = (await _mediator.Send(new AddPicture { FormFiles = Files, RoomId = RoomId })); ;
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePictureAsync(Guid id)
        {
            var result = await _mediator.Send(new DeletePicture() { Id = id });
            return Ok(result);
        }
    }
}
