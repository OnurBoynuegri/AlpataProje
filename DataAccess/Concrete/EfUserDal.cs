using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Repository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
	public class EfUserDal : EfEntityRepositoryBase<User>, IUserDal
	{
		public EfUserDal(AlpataProjeDbContext context) : base(context)
		{
		}
	}
}
