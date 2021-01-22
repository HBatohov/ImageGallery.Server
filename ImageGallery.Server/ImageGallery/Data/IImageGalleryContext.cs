using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ImageGallery.Models.Entities;

namespace ImageGallery.Data
{
    public interface IImageGalleryContext
    {
        DbSet<Room> Rooms { get; set; }
        DbSet<Picture> Pictures { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
