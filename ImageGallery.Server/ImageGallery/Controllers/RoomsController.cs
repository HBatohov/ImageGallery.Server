using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ImageGallery.Models.Entities;
using ImageGallery.Models.DTO;
using ImageGallery.DatabaseRequests.Rooms;
using ImageGallery.DatabaseRequests.Pictures;
using MediatR;

namespace ImageGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoomsAsync()
        {
            var result = await _mediator.Send(new GetAllRooms());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomAsync(Guid id)
        {
            var result = await _mediator.Send(new GetRoom() { Id = id });
            return Ok(result);
        }

        [HttpGet("{id}/Pictures")]
        public async Task<IActionResult> GetPicturesByRoomAsync(Guid id)
        {
            var result = await _mediator.Send(new GetPicturesByRoom() { RoomId = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoomAsync(Guid id, UpdateRoom updateRoom)
        {
            updateRoom.Id = id;
            var result = await _mediator.Send(updateRoom);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoomAsync(AddRoom addRoom)
        {
            var result = await _mediator.Send(addRoom);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomAsync(Guid id)
        {
            var result = await _mediator.Send(new DeleteRoom() { Id = id });
            return Ok(result);
        }
    }
}
