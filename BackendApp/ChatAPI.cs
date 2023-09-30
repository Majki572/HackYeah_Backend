using BackendApp.ChatLogic.BusinessLogic;
using BackendApp.ChatLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendApp;

public static class ChatAPI
{
    public static void UseChatAPI(this WebApplication app)
    {
        app.MapGet("/user/{userId:int}/chat",
            ([FromRoute] int userId, [FromServices] IChatLogic chatLogic) => chatLogic.GetChatInfo(userId));
        app.MapGet("/user/{userId:int}/chat/{chatId:int}",
            ([FromRoute] int userId, [FromRoute] int chatId, [FromServices] IChatLogic chatLogic) =>
                chatLogic.GetConversation(userId, chatId));
        app.MapPost("/user/{userId:int}/chat/{chatId:int}",
            ([FromRoute] int userId, [FromRoute] int chatId, [FromBody] SendMessageDTO sendMessageDto,
                [FromServices] IChatLogic chatLogic) => chatLogic.SendMessage(userId, chatId, sendMessageDto));
        app.MapPost("/user/{userId:int}/chat/{chatId:int}",
            ([FromRoute] int userId, [FromRoute] int chatId, [FromServices] IChatLogic chatLogic) =>
                chatLogic.MarkAsSeen(userId, chatId));
    }
}