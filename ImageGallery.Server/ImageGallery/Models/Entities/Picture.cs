using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models.Entities
{
    public class Picture : AbstractModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileMimeType { get; set; }
        public byte[] Data { get; set; }
        public DateTime CreateDate { get; set; }
        
        public Guid? RoomId { get; set; }
        public Room Room { get; set; }

    }
}
