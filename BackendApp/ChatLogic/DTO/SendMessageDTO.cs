namespace BackendApp.ChatLogic.DTO;

public class SendMessageDTO
{
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public DateTime Timestamp { get; set; }
    public string Text { get; set; }
}