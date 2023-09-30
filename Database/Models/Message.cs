﻿namespace Database.Models;

public class Message
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public DateTime Timestamp { get; set; }
    public string Text { get; set;  }
    private bool IsSeen { get; set; }
    public Conversation Conversation { get; set; }
}