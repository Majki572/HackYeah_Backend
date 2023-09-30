using System.Security.Cryptography;
using System.Text;
using Backend.ChatLogic.BusinessLogic;
using Backend.ChatLogic.DTO;
using Backend.DTO;
using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IChatLogic _chatLogic;
    private readonly ApplicationContext _applicationContext;
    
    public UserController(IChatLogic chatLogic, ApplicationContext applicationContext)
    {
        _chatLogic = chatLogic;
        _applicationContext = applicationContext;
    }

    [HttpPost("login")]
    public ActionResult<UserDTO> Login([FromBody] LoginDTO loginDto)
    {
        var user = _applicationContext.Users.FirstOrDefault(x => x.Username == loginDto.Username);
        if (user is null)
            return BadRequest("User with given username does not exist");

        if (!Utils.IsPasswordEqualHash(loginDto.Password, user.Password))
            return BadRequest("Incorrect password");

        return Ok(new UserDTO
        {
            Id = user.Id,
            UserName = user.Username,
            FridgeId = user.Fridge.Id
        });
    }
    
    [HttpGet("{userId:int}")]
    public UserDTO GetUser([FromRoute] int userId)
    {
        var user = _applicationContext.Users.Find(userId);
        if (user is null)
            return null;

        return new UserDTO
        {
            Id = user.Id,
            UserName = user.Username,
            FridgeId = user.Fridge.Id
        };
    }

    [HttpPost]
    public ActionResult CreateUser([FromBody] CreateUserDTO createUserDto)
    {
        var isUsernameAvailable =
            _applicationContext.Users.FirstOrDefault(x => x.Username == createUserDto.Username) is null;

        if (!isUsernameAvailable)
            return BadRequest("User with given username already exist.");

        var user = new User
        {
            Username = createUserDto.Username,
            Email = createUserDto.Email,
            Password = Utils.Hash(createUserDto.Password),
            Fridge = new Fridge()
            {
                Name = Guid.NewGuid().ToString(),
            }
        };

        _applicationContext.Users.Add(user);
        _applicationContext.SaveChanges();
        
        return Ok(user.Id);
    }

    [HttpGet("{userId:int}/chat")]
    public ChatInfoDTO GetChatInfo([FromRoute] int userId)
    {
        return _chatLogic.GetChatInfo(userId);
    }

    [HttpGet("{userId:int}/chat/{chatId:int}")]
    public ConversationDTO GetConversation([FromRoute] int userId, [FromRoute] int chatId)
    {
        return _chatLogic.GetConversation(userId, chatId);
    }

    [HttpPost("user/{userId:int}/chat/{chatId:int}")]
    public ActionResult SendMessage([FromRoute] int userId, [FromRoute] int chatId,
        [FromBody] SendMessageDTO sendMessageDto)
    {
        _chatLogic.SendMessage(userId, chatId, sendMessageDto);
        return Ok();
    }

    [HttpPost("user/{userId:int}/chat/{chatId:int}/mark")]
    public ActionResult MarkAsSeen([FromRoute] int userId, [FromRoute] int chatId)
    {
        _chatLogic.MarkAsSeen(userId, chatId);
        return Ok();
    }
}