using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ImageGallery.Models.DTO;
using ImageGallery.Models.Entities;

namespace ImageGallery.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomDTO>();
            CreateMap<RoomDTO, Room>();

            CreateMap<Picture, PictureDTO>();
            CreateMap<PictureDTO, Picture>();
        }
    }
}
