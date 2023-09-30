using BackendApp.ChatLogic.DTO;

namespace BackendApp.ChatLogic.BusinessLogic;

public class ChatLogicService : IChatLogic
{
    public ChatInfoDTO GetChatInfo(int userId)
    {
        return new ChatInfoDTO
        {
            UserId = userId,
            UserName = "radek",
            Conversations = new List<ConversationDTO>
            {
                new ConversationDTO()
                {

                }
            }
        };
        
        
    }

    public ConversationDTO GetConversation(int userId, int chatId)
    {
        return new ConversationDTO();
    }

    public void SendMessage(int userId, int chatId, SendMessageDTO sendMessageDto)
    {
        throw new NotImplementedException();
    }

    public void MarkAsSeen(int userId, int chatId)
    {
        throw new NotImplementedException();
    }
}