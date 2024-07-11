using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	internal class UserMeetingManager : IUserMeetingService
	{
		private readonly IUserMeetingDal _userMeetingDal;

		public UserMeetingManager(IUserMeetingDal userMeetingDal)
		{
			_userMeetingDal = userMeetingDal;
		}

		public async Task AddUserMeetingAsync(UserMeeting userMeeting)
		{
			await _userMeetingDal.Add(userMeeting);
		}

		public async Task<List<Meeting>> GetMeetingsByUserIdAsync(int userId)
		{
			return await _userMeetingDal.Query()
			.Where(um => um.UserId == userId)
			.Select(um => um.Meeting)
			.ToListAsync();
		}

		public async Task<List<User>> GetUsersByMeetingIdAsync(int meetingId)
		{
			return await _userMeetingDal.Query()
				.Where(um => um.MeetingId == meetingId)
				.Select(um => um.User)
				.ToListAsync();
		}
	}
}
