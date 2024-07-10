using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Repository;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
	public class EfUserDal : EfEntityRepositoryBase<User>, IUserDal
	{
		private readonly AlpataProjeDbContext _context;
		public EfUserDal(AlpataProjeDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<User> GetByEmail(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
		}
	}
}
