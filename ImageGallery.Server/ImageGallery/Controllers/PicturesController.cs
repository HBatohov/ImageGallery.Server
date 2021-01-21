using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImageGallery.Data;
using ImageGallery.Models.Entities;
using ImageGallery.DatabaseRequests.Pictures;
using MediatR;

namespace ImageGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ImageGalleryContext _context;

        public PicturesController(IMediator mediator, ImageGalleryContext context)
        {
            _mediator = mediator;
            _context = context;
        }

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

        [HttpPost]
        public async Task<IActionResult> AddPictureAsync(AddPicture addPicture)
        {
            var result = await _mediator.Send(addPicture);
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
