namespace Core;

public class Comment
{
    public string Text { get; set; } = "";
    public string UserName { get; set; } = "";
    public string UserRole { get; set; } = "";
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}