using Microsoft.EntityFrameworkCore;
using StockaProSSO.Core.Domain;
using StockaProSSO.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockaProSSO.Core.Services
{

    public class Repository<T, Tid> : IRepository<T, Tid> where T : BaseEntity<Tid>
    {
        private readonly StockaProContext context;

        public Repository(StockaProContext _context)
        {
            this.context = _context;
        }
        public void Delete(T entity)
        {
            GetEntity().Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
           
            return GetEntity().AsQueryable();
        }

        public void Insert(T entity)
        {

            GetEntity().Add(entity);
        }



        private DbSet<T> GetEntity()
        {
            return this.context.Set<T>();
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            if (this.context != null)
            {
                this.context.Dispose();
            }
        }


        public async virtual Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }


        public void Update(T entity)
        {
            var context = this.context as StockaProContext;
            var entry = context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;

        }


        public T Find(Tid Id)
        {
            return GetEntity().Find(Id);
        }
    }
}
