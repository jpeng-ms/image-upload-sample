using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Repositories;

namespace SampleWebApiAspNetCore.Services
{
    public class SeedDataService : ISeedDataService
    {
        public void Initialize(ImageDbContext context)
        {
            // test
            context.ImageItems.Add(new ImageEntity() { Type = "png", Name = "image" });

            context.SaveChanges();
        }
    }
}
