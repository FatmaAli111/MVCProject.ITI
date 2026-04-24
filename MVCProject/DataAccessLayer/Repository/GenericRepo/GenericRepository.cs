using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace MvcProject.iti.DataAccessLayer.Repository.GenericRepo
{
    public class GenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual IQueryable<T> GetTableNoTracking()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public virtual IQueryable<T> GetTableAsTracking()
        {
            return _dbContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public virtual void AddRange(ICollection<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public virtual void UpdateRange(ICollection<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public virtual void DeleteRange(ICollection<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();
        }
    }
}
