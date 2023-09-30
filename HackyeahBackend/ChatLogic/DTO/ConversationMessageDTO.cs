namespace Backend.ChatLogic.DTO;

public class ConversationMessageDTO
{
    public int SenderId { get; set; }
    public string SenderName { get; set; }
    public int ReceiverId { get; set; }
    public string ReceiverName { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Text { get; set; }
}