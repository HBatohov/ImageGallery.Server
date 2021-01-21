using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using ImageGallery.Models;
using ImageGallery.Models.Entities;

namespace ImageGallery.Data
{
    public class ImageGalleryContext : DbContext, IImageGalleryContext
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
                .OnDelete(DeleteBehavior.SetNull);  // HB - or .Cascade
        }
    }
}
