using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class UserMeeting:IEntity
	{
        public int UserId { get; set; }
		public User User { get; set; }

		public int MeetingId { get; set; }	
		public Meeting Meeting { get; set; }
	}
}
