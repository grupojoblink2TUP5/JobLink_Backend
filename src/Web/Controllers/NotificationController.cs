using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs.Notification.Request;
using Microsoft.AspNetCore.Authorization;
using Domain.Exceptions;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var notifications = _notificationService.GetAllNotifications();

            return Ok(notifications);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var notification = _notificationService.GetNotificationById(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        [HttpGet("user/{userId:int}")]
        public IActionResult GetByUserId([FromRoute] int userId)
        {
            var notifications = _notificationService.GetNotificationsByUserId(userId);

            return Ok(notifications);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] CreateNotificationRequest request)
        {
            var result = _notificationService.CreateNotification(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result
            );
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] UpdateNotificationRequest request
        )
        {
            try
            {
                var result = _notificationService.UpdateNotification(id, request);

                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var success = _notificationService.DeleteNotification(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
