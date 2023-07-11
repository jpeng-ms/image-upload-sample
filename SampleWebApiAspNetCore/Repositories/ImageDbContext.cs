using Microsoft.EntityFrameworkCore;
using SampleWebApiAspNetCore.Entities;

namespace SampleWebApiAspNetCore.Repositories
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions<ImageDbContext> options)
            : base(options)
        {
        }

        public DbSet<ImageEntity> ImageItems { get; set; } = null!;
    }
}
