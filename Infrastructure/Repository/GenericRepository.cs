using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Now Implemenet Interface 
namespace Infrastructure.Repository
{
   public class GenericRepository<T> : IGenricRepository<T> where T : class
    {
        private readonly DataContext _context;
        private DbSet<T> Table = null;

        public GenericRepository(DataContext context)  // Dependency Injection
        {
            this._context = context;
            Table = _context.Set<T>();  // to allow you save datbase commin in table Dbset
        }
        public void Delete(object id)
        {
            T existing = GetById(id);
            Table.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return Table.ToList();
        }

        public T GetById(object id)
        {
            return Table.Find(id);
        }

        public void Insert(T entity)
        {
            Table.Add(entity);
        }

        public void Update(T entity)
        {
            Table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
