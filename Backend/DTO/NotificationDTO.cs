namespace Backend.DTO;

public class NotificationDTO
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Timestamp { get; set; }
    public bool IsSeen { get; set; }
}