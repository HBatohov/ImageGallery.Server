using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models
{
    public abstract class AbstractModel
    {
        public Guid Id { get; set; }
    }
}
