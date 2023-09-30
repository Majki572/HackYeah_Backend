namespace Database.Models;

public class Conversation
{
    public int Id { get; set; }
    public int User1Id { get; set; }
    public int User2Id { get; set; }
    public bool IsSeenUser1 { get; set; }
    public bool IsSeenUser2 { get; set; }
    public ICollection<Message> Messages { get; set; }
}