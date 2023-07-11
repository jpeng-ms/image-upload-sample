using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Helpers;
using SampleWebApiAspNetCore.Models;
using System.Linq.Dynamic.Core;

namespace SampleWebApiAspNetCore.Repositories
{
    public class ImageSqlRepository : IImageRepository
    {
        private readonly ImageDbContext _ImageDbContext;

        public ImageSqlRepository(ImageDbContext ImageDbContext)
        {
            _ImageDbContext = ImageDbContext;
        }

        public ImageEntity GetSingle(int id)
        {
            return _ImageDbContext.ImageItems.FirstOrDefault(x => x.Id == id);
        }

        public void Add(ImageEntity item)
        {
            _ImageDbContext.ImageItems.Add(item);
        }

        public void Delete(int id)
        {
            ImageEntity ImageItem = GetSingle(id);
            _ImageDbContext.ImageItems.Remove(ImageItem);
        }

        public ImageEntity Update(int id, ImageEntity item)
        {
            _ImageDbContext.ImageItems.Update(item);
            return item;
        }

        public IQueryable<ImageEntity> GetAll(QueryParameters queryParameters)
        {
            IQueryable<ImageEntity> _allItems = _ImageDbContext.ImageItems.OrderBy(queryParameters.OrderBy,
              queryParameters.IsDescending());

            if (queryParameters.HasQuery())
            {
                _allItems = _allItems
                    .Where(x => x.Name.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return _allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public int Count()
        {
            return _ImageDbContext.ImageItems.Count();
        }

        public bool Save()
        {
            return (_ImageDbContext.SaveChanges() >= 0);
        }

        private ImageEntity GetRandomItem(string type)
        {
            return _ImageDbContext.ImageItems
                .Where(x => x.Type == type)
                .OrderBy(o => Guid.NewGuid())
                .FirstOrDefault();
        }
    }
}
