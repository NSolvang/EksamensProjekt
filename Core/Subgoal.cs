namespace Core;

public class Subgoal
{
 
    public int SubgoalID { get; set; }
    
    public string Name { get; set; }
    
    public DateTime Date { get; set; }

    public string Responsible { get; set; }
    
    public string Deadline { get; set; }

    public string Status { get; set; } = "Mangler";

    public List<Comment> Comments { get; set; } = new List<Comment>();
    
    public bool Approval { get; set; } = false; 
}