using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models.DTO
{
    public class PictureWithDataDTO : PictureDTO
    {
        public string FileMimeType { get; set; }
        public byte[] Data { get; set; }
    }
}
