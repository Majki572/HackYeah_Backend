using BackendApp.ChatLogic.BusinessLogic;
using BackendApp.ChatLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendApp;

public static class ChatAPI
{
    public static void UseChatAPI(this WebApplication app)
    {
        app.MapGet("/user/{userId:int}/chat", (int userId, IChatLogic chatLogic) => chatLogic.GetChatInfo(userId));
        app.MapGet("/user/{userId:int}/chat/{chatId:int}", (int userId, int chatId, IChatLogic chatLogic) => chatLogic.GetConversation(userId, chatId));
        app.MapPost("/user/{userId:int}/chat/{chatId:int}", (int userId,int chatId, [FromBody]SendMessageDTO sendMessageDto, IChatLogic chatLogic) => chatLogic.SendMessage(userId, chatId, sendMessageDto));
        app.MapPost("/user/{userId:int}/chat/{chatId:int}", (int userId, int chatId, IChatLogic chatLogic) => chatLogic.MarkAsSeen(userId, chatId));
    }
}