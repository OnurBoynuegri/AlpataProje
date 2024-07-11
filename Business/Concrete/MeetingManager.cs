using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class MeetingManager : IMeetingService
	{
		private readonly IMeetingDal _meetingDal;
		private readonly IUserMeetingDal _userMeetingDal;
		public MeetingManager(IMeetingDal meetingDal, IUserMeetingDal userMeetingDal)
		{
			_meetingDal = meetingDal;
			_userMeetingDal = userMeetingDal;
		}

		public async Task AddMeeting(Meeting meeting)
		{
			await _meetingDal.Add(meeting);
		}

		public async Task UpdateMeeting(Meeting meeting)
		{
			await _meetingDal.Update(meeting);
		}

		public async Task DeleteMeeting(int id)
		{
			var meeting = await GetMeetingById(id);
			await _meetingDal.Delete(meeting);
		}

		public async Task<IEnumerable<Meeting>> GetAllMeetings()
		{
			return await _meetingDal.GetAll();
		}

		public async Task<Meeting> GetMeetingById(int id)
		{
			return await _meetingDal.GetById(id);
		}

	}
}
