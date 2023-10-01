namespace Backend.DTO;

public class NotificationDTO
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsSeen { get; set; }
    public string UrlToMarkAsSeen { get; set; }
}