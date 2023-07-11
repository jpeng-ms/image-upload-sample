using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Models;

namespace SampleWebApiAspNetCore.Repositories
{
    public interface IImageRepository
    {
        ImageEntity GetSingle(int id);
        void Add(ImageEntity item);
        void Delete(int id);
        ImageEntity Update(int id, ImageEntity item);
        IQueryable<ImageEntity> GetAll(QueryParameters queryParameters);
        int Count();
        bool Save();
    }
}
