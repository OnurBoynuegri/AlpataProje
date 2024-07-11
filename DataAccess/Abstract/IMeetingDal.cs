using DataAccess.Repository.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
	public interface IMeetingDal : IEntityRepository<Meeting>
	{
		Task<Meeting> Get(Expression<Func<Meeting, bool>> filter, string includeProperties = null);
	}
}
