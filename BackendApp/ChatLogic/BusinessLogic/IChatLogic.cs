using BackendApp.ChatLogic.DTO;

namespace BackendApp.ChatLogic.BusinessLogic;

public interface IChatLogic
{
    public ChatInfoDTO GetChatInfo(int userId);
    public ConversationDTO GetConversation(int userId, int chatId);
    public void SendMessage(int userId, int chatId, SendMessageDTO sendMessageDto);
    public void MarkAsSeen(int userId, int chatId);
}