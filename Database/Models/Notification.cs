namespace Database.Models;

public class Notification
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsSeen { get; set; }
    public string Text { get; set; }
    public string _identity1 { get; set; }
    public string _identity2 { get; set; }
}