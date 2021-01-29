using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using ImageGallery.Models.Entities;

namespace ImageGallery.Data
{
    public class ImageGalleryContext : IdentityDbContext<ApplicationUser>, IImageGalleryContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public ImageGalleryContext()
        {
        }

        public ImageGalleryContext(DbContextOptions<ImageGalleryContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>()
                .ToTable("Rooms").HasKey(p => p.Id);
            modelBuilder.Entity<Room>()
                .Property(p => p.Name).IsRequired().HasMaxLength(260);

            modelBuilder.Entity<Picture>()
                .ToTable("Pictures").HasKey(p => p.Id);
            modelBuilder.Entity<Picture>()
                .Property(p => p.Name).IsRequired().HasMaxLength(260);
            modelBuilder.Entity<Picture>()
                .Property(p => p.FileMimeType).IsRequired().HasMaxLength(260);
            modelBuilder.Entity<Picture>()
                .HasOne<Room>(p => p.Room)
                .WithMany(t => t.Pictures)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.FirsName).HasMaxLength(60);
            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.LastName).HasMaxLength(60);
        }
    }
}
