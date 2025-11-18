namespace Aggregator.DTOs;

public class UserCommentDto
{
    //Todo needed to create some event title adding flow into the user comments dashboard method in Aggregator
    //so the user can see which comment is used for certain event 
    public int CommentId { get; set; }
    public string EventId { get; set; }
    //public string EventTitle { get; set; }
    public string CommentText { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsChanged { get; set; } = false;
}