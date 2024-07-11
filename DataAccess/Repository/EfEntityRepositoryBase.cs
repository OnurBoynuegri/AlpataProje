using DataAccess.Context;
using DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
	public class EfEntityRepositoryBase<T> : IEntityRepository<T> where T : class
	{
		protected readonly AlpataProjeDbContext _context;
		protected readonly DbSet<T> _dbSet;
        public EfEntityRepositoryBase(AlpataProjeDbContext context)
        {
            _context = context;
			_dbSet= _context.Set<T>();
        }
        public async Task Add(T entity)
		{
			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(T entity)
		{
			_dbSet.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T> GetById(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task Update(T entity)
		{
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
