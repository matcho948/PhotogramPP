using Microsoft.AspNetCore.Mvc;
using Photogram.Data;
using Photogram.Models;

namespace Photogram.Controllers
{
    [ApiController]
    [Route("/api/notification")]
    public class NotificationController : ControllerBase
    {
        private readonly IPhotogramRepo _repo;
        public NotificationController(IPhotogramRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("/GetAllNotificationsForUser/{userId}")]
        public async Task<ActionResult<List<Notifications>>> GetAllNotificationsForUser(int userId)
        {
            if (userId != null)
            {
                var notifications = await _repo.GetAllNotificationForUser(userId);
                if (notifications.Any())
                {
                    notifications.ForEach(p => p.User.Photos = null);
                    return Ok(notifications);
                }
            }
            return BadRequest();
;        }
    }
}
