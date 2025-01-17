﻿using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Meeting:IEntity
	{
		public int Id { get; set; }
		public string? Name { get; set; }
        public string? Description { get; set; }
        public string? FileName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<UserMeeting> UserMeetings { get; set; }
    }
}
