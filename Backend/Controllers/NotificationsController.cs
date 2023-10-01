using Backend.DTO;
using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly ApplicationContext _applicationContext;

    public NotificationsController(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    [HttpPost("{id:int}/user/{userId:int}")]
    public ActionResult MarkNotificationAsSeen([FromRoute] int id, [FromRoute] int userId)
    {
        var notification = _applicationContext.Notifications.FirstOrDefault(n => n.Id == id && n.UserId == userId);
        if (notification is null)
            return BadRequest("Notification with given id and userId does not exist.");

        if (notification.IsSeen)
            return Ok();

        notification.IsSeen = true;
        _applicationContext.Notifications.Update(notification);
        _applicationContext.SaveChanges();

        return Ok();
    }

    [HttpGet]
    public ActionResult<IEnumerable<NotificationDTO>> GetNotifications([FromQuery] int userId)
    {
        GenerateNotifications(userId);
        return _applicationContext.Notifications.Where(n => n.UserId == userId).Select(n => new NotificationDTO
        {
            Id = n.Id,
            Timestamp = n.Timestamp,
            Text = n.Text,
            IsSeen = n.IsSeen,
            UrlToMarkAsSeen = $"/api/notifications/{n.Id}/user/{userId}"
        }).OrderByDescending(ndto => ndto.Timestamp).ToList();
    }

    private void GenerateNotifications(int userId)
    {
        GenerateNotificationsFromChat(userId);
        GenerateNotificationsFromProducts(userId);
    }

    private void GenerateNotificationsFromChat(int userId)
    {
        var chats = _applicationContext.Conversations.Where(x => x.User1Id == userId || x.User2Id == userId).ToList();
        if (chats is null || !chats.Any())
            return;

        foreach (var chat in chats)
        {
            var lastMessageReceiverId = chat.Messages.ToList()[chat.Messages.ToList().Count - 1].ReceiverId;

            if (lastMessageReceiverId != userId)
                continue;

            if (chat.User1Id == lastMessageReceiverId && chat.IsSeenUser1)
                continue;

            if (chat.User2Id == lastMessageReceiverId && chat.IsSeenUser2)
                continue;

            var sender =
                _applicationContext.Users.Find(chat.User1Id == lastMessageReceiverId ? chat.User2Id : chat.User1Id);
            var messageDateTime = chat.Messages.ToList()[chat.Messages.ToList().Count - 1].Timestamp;
            var notificationMessage = $"You have unread message from {sender.Username} - {messageDateTime:g}";

            var existingNotification = _applicationContext.Notifications.FirstOrDefault(x =>
                x._identity1 == notificationMessage && x._identity2 == $"{userId}");

            if (existingNotification != null)
                continue;

            var notification = new Notification
            {
                UserId = userId,
                Timestamp = messageDateTime,
                Text = notificationMessage,
                IsSeen = false,
                _identity1 = notificationMessage,
                _identity2 = $"{userId}"
            };

            _applicationContext.Notifications.Add(notification);
            _applicationContext.SaveChanges();
        }
    }

    private void GenerateNotificationsFromProducts(int userId)
    {
        var user = _applicationContext.Users.Find(userId);
        _applicationContext.Entry(user).Reference(u => u.Fridge).Load();
        _applicationContext.Entry(user.Fridge).Collection(f => f.Products).Load();
        if (user is null)
            return;

        foreach (var productForNotifications in user.Fridge.Products.Where(p =>
                     (DateTime.Now - p.ExpirationDate).TotalDays < 3.0))
        {
            var notificationDateTime = productForNotifications.ExpirationDate.AddDays(-2);
            var notificationMessage = $"Your product {productForNotifications.Name} will expire in two days.";

            var existingNotification = _applicationContext.Notifications.FirstOrDefault(n =>
                n.UserId == userId && n._identity1 == notificationMessage &&
                n._identity2 == $"{userId} {productForNotifications.Id}");
            
            if (existingNotification is not null)
                continue;

            var notification = new Notification
            {
                UserId = userId,
                Timestamp = notificationDateTime,
                Text = notificationMessage,
                IsSeen = false,
                _identity1 = notificationMessage,
                _identity2 = $"{userId} {productForNotifications.Id}"
            };

            _applicationContext.Notifications.Add(notification);
            _applicationContext.SaveChanges();
        }
    }
}