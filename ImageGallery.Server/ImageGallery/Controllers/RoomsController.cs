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
using ImageGallery.Features.Rooms;
using ImageGallery.Features.Pictures;

namespace ImageGallery.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AsyncLightQuery(forcePagination: false, defaultPageSize: AppConstans.DEFAULT_PAGE_SIZE)]
        [ProducesResponseType(typeof(PaginationResult<RoomDTO>), 200)]
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

        [AsyncLightQuery(forcePagination: false, defaultPageSize: AppConstans.DEFAULT_PAGE_SIZE)]
        [ProducesResponseType(typeof(PaginationResult<PictureDTO>), 200)]
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
