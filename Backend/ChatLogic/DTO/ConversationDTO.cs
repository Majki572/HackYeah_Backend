namespace Backend.ChatLogic.DTO;

public class ConversationDTO
{
    public int ChatId { get; set; }
    public int User1Id { get; set; }
    public string User1Name { get; set; }
    public int User2Id { get; set; }
    public string User2Name { get; set; }
    public bool IsSeen { get; set; }
    public IEnumerable<ConversationMessageDTO> Messages { get; set; }
}