﻿using DataAccess.Repository.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
	public interface IUserMeetingDal:IEntityRepository<UserMeeting>
	{
		Task<IEnumerable<UserMeeting>> GetAllWithDetails();
		IQueryable<UserMeeting> Query();
	}
}
