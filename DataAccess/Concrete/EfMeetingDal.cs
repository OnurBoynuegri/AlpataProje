using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Repository;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
	public class EfMeetingDal : EfEntityRepositoryBase<Meeting>, IMeetingDal
	{
		public EfMeetingDal(AlpataProjeDbContext context) : base(context)
		{
		}
		public async Task<Meeting> Get(Expression<Func<Meeting, bool>> filter, string includeProperties = null)
		{
			var query = _dbSet.AsQueryable();

			if (includeProperties != null)
			{
				foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty.Trim());
				}
			}

			return await query.FirstOrDefaultAsync(filter);
		}
	}
}
