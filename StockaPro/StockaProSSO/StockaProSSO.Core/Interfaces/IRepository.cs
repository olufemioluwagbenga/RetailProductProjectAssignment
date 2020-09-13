using StockaProSSO.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockaProSSO.Core.Interfaces
{
    public interface IRepository<T, Tid> where T : BaseEntity<Tid>
    {

        IQueryable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        int SaveChanges();
        T Find(Tid Id);
        Task<int> SaveChangesAsync();
    }
}
