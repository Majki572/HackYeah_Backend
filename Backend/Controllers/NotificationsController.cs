using Backend.DTO;
using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly ApplicationContext _applicationContext;

    public NotificationsController(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<NotificationDTO>> GetNotifications([FromQuery] int userId)
    {
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
        if (user is null)
            return;

        foreach (var productForNotifications in user.Fridge.Products.Where(p => (DateTime.Now - p.ExpirationDate).TotalDays < 1.0))
        {
            
        }
    }
}