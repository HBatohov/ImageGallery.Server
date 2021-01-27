using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using ImageGallery.Models.DTO;
using ImageGallery.Models.Entities;
using ImageGallery.Features.Rooms;
using ImageGallery.Features.Pictures;

namespace ImageGallery
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomDTO>();
            CreateMap<RoomDTO, Room>();

            CreateMap<AddRoom, Room>();
            CreateMap<UpdateRoom, Room>();

            CreateMap<Picture, PictureDTO>();
            CreateMap<PictureDTO, Picture>();

            CreateMap<Picture, PictureDataDTO>();
            CreateMap<PictureDataDTO, Picture>();

            CreateMap<Picture, PictureWithDataDTO>();
            CreateMap<PictureWithDataDTO, Picture>();

            CreateMap<AddPicture, Picture>();
            CreateMap<UpdatePicture, Picture>();
        }
    }
}
