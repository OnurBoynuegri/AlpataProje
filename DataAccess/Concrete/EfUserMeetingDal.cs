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
	public class EfUserMeetingDal : EfEntityRepositoryBase<UserMeeting>, IUserMeetingDal
	{
		private readonly AlpataProjeDbContext _context;
		public EfUserMeetingDal(AlpataProjeDbContext context) : base(context)
		{
			_context = context;
		}
		public async Task<IEnumerable<UserMeeting>> GetAllWithDetails()
		{
			return await _context.UserMeetings
				.Include(um => um.User)
				.Include(um => um.Meeting)
				.ToListAsync();
		}
		public IQueryable<UserMeeting> Query()
		{
			return _context.UserMeetings.AsQueryable();
		}
	}
}
