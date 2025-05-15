namespace Core;

public class Comment
{
    public int CommentId { get; set; }
    
    public string Text { get; set; }
    
    public User Author { get; set; }
    
    public DateOnly Date { get; set; }
}