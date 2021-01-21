using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models.DTO
{
    public class PictureDTO : AbstractModelDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        public Guid? RoomId { get; set; }
    }
}
