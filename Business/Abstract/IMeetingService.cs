using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IMeetingService
	{
		Task<IEnumerable<Meeting>> GetAllMeetings();
		Task<Meeting> GetMeetingById(int id);
		Task AddMeeting(Meeting meeting);
		Task UpdateMeeting(Meeting meeting);
		Task DeleteMeeting(int id);
	}
}
