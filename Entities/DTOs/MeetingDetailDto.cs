using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
	public class MeetingDetailDto
	{
		public string UserName { get; set; }
		public string UserSurname { get; set; }
		public string MeetingName { get; set; }
        public string Description { get; set; }
		public string FileName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
