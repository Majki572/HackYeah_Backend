using Backend.ChatLogic.BusinessLogic;
using Backend.ChatLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IChatLogic _chatLogic;

    public UserController(IChatLogic chatLogic)
    {
        _chatLogic = chatLogic;
    }

    [HttpGet("/{userId:int}/chat")]
    public ChatInfoDTO GetChatInfo([FromRoute] int userId)
    {
        return _chatLogic.GetChatInfo(userId);
    }

    [HttpGet("/{userId:int}/chat/{chatId:int}")]
    public ConversationDTO GetConversation([FromRoute] int userId, [FromRoute] int chatId)
    {
        return _chatLogic.GetConversation(userId, chatId);
    }

    [HttpPost("/user/{userId:int}/chat/{chatId:int}")]
    public ActionResult SendMessage([FromRoute] int userId, [FromRoute] int chatId,
        [FromBody] SendMessageDTO sendMessageDto)
    {
        _chatLogic.SendMessage(userId, chatId, sendMessageDto);
        return Ok();
    }

    [HttpPost("/user/{userId:int}/chat/{chatId:int}/mark")]
    public ActionResult MarkAsSeen([FromRoute] int userId, [FromRoute] int chatId)
    {
        _chatLogic.MarkAsSeen(userId, chatId);
        return Ok();
    }
}