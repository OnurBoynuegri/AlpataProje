using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IUserMeetingService
	{
		Task AddUserMeetingAsync(UserMeeting userMeeting);
		Task<List<User>> GetUsersByMeetingIdAsync(int meetingId);
		Task<List<Meeting>> GetMeetingsByUserIdAsync(int userId);
	}
}
