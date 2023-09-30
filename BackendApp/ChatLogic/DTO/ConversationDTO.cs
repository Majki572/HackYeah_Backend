namespace BackendApp.ChatLogic.DTO;

public class ConversationDTO
{
    public int ChatId { get; set; }
    public int SenderId { get; set; }
    public string SenderName { get; set; }
    public int ReceiverId { get; set; }
    public string ReceiverName { get; set; }
    public bool IsSeen { get; set; }
    public IEnumerable<ConversationMessageDTO> Messages { get; set; }
}