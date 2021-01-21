﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ImageGallery.Models.DTO;
using ImageGallery.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ImageGallery.Data;

namespace ImageGallery.DatabaseRequests.Rooms
{
    public class AddRoom : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public class AddRoomHandler : IRequestHandler<AddRoom, Guid>
        {
            private readonly IImageGalleryContext _context;
            private readonly IMapper _mapper;

            public AddRoomHandler(IImageGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(AddRoom addRoom, CancellationToken cancellationToken)
            {
                // HB - validation
                var room = _mapper.Map<Room>(addRoom);
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync(cancellationToken);
                return room.Id;
            }
        }
    }
}