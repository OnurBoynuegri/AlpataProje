using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MeetingsController : ControllerBase
	{
		private readonly IMeetingService _meetingService;

		public MeetingsController(IMeetingService meetingService)
		{
			_meetingService = meetingService;
		}

		[HttpGet("GetAllMeetings")]
		public async Task<IActionResult> GetAllMeetings()
		{
			var meetings = await _meetingService.GetAllMeetings();
			return Ok(meetings);
		}

		[HttpGet("GetMeetingById")]
		public async Task<IActionResult> GetMeetingById(int id)
		{
			var meeting = await _meetingService.GetMeetingById(id);
			if (meeting == null)
			{
				return NotFound();
			}
			return Ok(meeting);
		}

		[HttpPost("AddMeeting")]
		public async Task<IActionResult> AddMeeting([FromBody] Meeting meeting)
		{
			if (meeting == null)
			{
				return BadRequest();
			}
			await _meetingService.AddMeeting(meeting);
			return CreatedAtAction(nameof(GetMeetingById), new { id = meeting.Id }, meeting);
		}

		[HttpPut("UpdateMeeting")]
		public async Task<IActionResult> UpdateMeeting(int id, [FromBody] Meeting meeting)
		{
			if (id != meeting.Id)
			{
				return BadRequest();
			}
			await _meetingService.UpdateMeeting(meeting);
			return NoContent();
		}

		[HttpDelete("DeleteMeeting")]
		public async Task<IActionResult> DeleteMeeting(int id)
		{
			await _meetingService.DeleteMeeting(id);
			return NoContent();
		}

		[HttpGet("GetMeetingDetails")]
		public async Task<IActionResult> GetMeetingDetails()
		{
			var meetingDetails = await _meetingService.GetMeetingDetails();
			return Ok(meetingDetails);
		}

	}
}
