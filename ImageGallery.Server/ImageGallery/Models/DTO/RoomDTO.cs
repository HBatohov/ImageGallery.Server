using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGallery.Models;

namespace ImageGallery.Models.DTO
{
    public class RoomDTO : AbstractModelDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<PictureDTO> Pictures { get; set; } = new List<PictureDTO>();
    }
}
