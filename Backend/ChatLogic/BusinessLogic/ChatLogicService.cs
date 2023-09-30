using Backend.ChatLogic.DTO;
using Database.Models;

namespace Backend.ChatLogic.BusinessLogic;

public class ChatLogicService : IChatLogic
{
    private readonly ApplicationContext _applicationContext;

    public ChatLogicService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public ChatInfoDTO GetChatInfo(int userId)
    {
        var user = _applicationContext.Users.Find(userId);
        if (user is null)
            return null;

        var chatInfoDTO = new ChatInfoDTO
        {
            UserId = user.Id,
            UserName = user.Username,
            Conversations = new List<ConversationDTO>()
        };

        var conversations = _applicationContext.Conversations
            .Where(x => x.User1Id == userId || x.User2Id == userId)
            .Select(x => x.Id).ToList();

        foreach (var conversationId in conversations)
        {
            ((List<ConversationDTO>)chatInfoDTO.Conversations).Add(GetConversation(userId, conversationId));
        }

        return chatInfoDTO;
    }

    public ConversationDTO GetConversation(int userId, int chatId)
    {
        var conversation = _applicationContext.Conversations
            .AsQueryable()
            .Where(x => x.User1Id == userId || x.User2Id == userId)
            .FirstOrDefault(x => x.Id == chatId);

        if (conversation is null)
            return null;

        var user1Id = conversation.User1Id < conversation.User2Id ? conversation.User1Id : conversation.User2Id;
        var user2Id = conversation.User1Id < conversation.User2Id ? conversation.User2Id : conversation.User1Id;
        var user1 = _applicationContext.Users.Find(user1Id);
        var user2 = _applicationContext.Users.Find(user2Id);
        
        var conversationDTO = new ConversationDTO
        {
            User1Id = user1.Id,
            User1Name = user1.Username,
            User2Id = user2.Id,
            User2Name = user2.Username,
            ChatId = chatId,
            Messages = _applicationContext.Messages.AsQueryable().Where(m => m.ReceiverId == user1Id || m.SenderId == user1Id)
                .Where(m => m.ReceiverId == user2Id || m.SenderId == user2Id)
                .Where(m => m.Conversation.Id == chatId)
                .Select(m => new ConversationMessageDTO
                {
                    SenderId = m.SenderId,
                    SenderName = m.SenderId == user1Id ? user1.Username : user2.Username,
                    ReceiverId = m.ReceiverId,
                    ReceiverName = m.ReceiverId == user1Id ? user1.Username : user2.Username,
                    Text = m.Text,
                    TimeStamp = m.Timestamp
                })
                .OrderBy(m => m.TimeStamp)
                .ToList(),
            IsSeen = user1Id == userId ? conversation.IsSeenUser1 : conversation.IsSeenUser2
        };
        
        return conversationDTO;
    }

    public void SendMessage(int userId, int chatId, SendMessageDTO sendMessageDto)
    {
        var chat = _applicationContext.Conversations
            .Where(x => x.Id == chatId)
            .Where(x => x.User1Id == userId || x.User2Id == userId)
            .FirstOrDefault(x => x.Id == chatId);

        if (sendMessageDto.SenderId != chat.User1Id && sendMessageDto.SenderId != chat.User2Id)
            throw new Exception("Incorrect user for chat");
        if (sendMessageDto.ReceiverId != chat.User1Id && sendMessageDto.ReceiverId != chat.User2Id)
            throw new Exception("Incorrect user for chat");

        var message = new Message
        {
            Conversation = chat,
            Timestamp = sendMessageDto.Timestamp,
            ReceiverId = sendMessageDto.ReceiverId,
            SenderId = sendMessageDto.SenderId,
            Text = sendMessageDto.Text,
        };

        _applicationContext.Messages.Add(message);
        _applicationContext.SaveChanges();
    }

    public void MarkAsSeen(int userId, int chatId)
    {
        var chat = _applicationContext.Conversations
            .Where(x => x.Id == chatId)
            .Where(x => x.User1Id == userId || x.User2Id == userId)
            .FirstOrDefault(x => x.Id == chatId);
        
        if (chat is null)
            return;

        if (userId == chat.User1Id)
            chat.IsSeenUser1 = true;
        else
            chat.IsSeenUser2 = true;

        _applicationContext.Conversations.Update(chat);
        _applicationContext.SaveChanges();
    }
}