using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models.Entities
{
    public class Room : AbstractModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Picture> Pictures { get; set; } = new List<Picture>();
    }
}
