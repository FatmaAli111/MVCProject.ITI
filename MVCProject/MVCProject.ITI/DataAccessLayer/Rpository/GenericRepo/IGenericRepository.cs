using Microsoft.EntityFrameworkCore.Storage;

namespace MvcProject.iti.DataAccessLayer.Repository.GenericRepo
{
    public interface IGenericRepository<T> where T:class
    {
        T GetById(int id);
        void SaveChanges();
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
        void Add(T entity);
        void AddRange(ICollection<T> entities);
        void Update(T entity);
        void UpdateRange(ICollection<T> entities);
        void Delete(T entity);
        void DeleteRange(ICollection<T> entities);

    }
}
