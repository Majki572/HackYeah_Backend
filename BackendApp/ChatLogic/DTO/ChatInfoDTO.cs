namespace BackendApp.ChatLogic.DTO;

public class ChatInfoDTO
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public IEnumerable<ConversationDTO> Conversations { get; set; }
}