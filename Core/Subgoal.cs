namespace Core;

public class Subgoal
{
 
    public int SubgoalID { get; set; }
    
    public string Name { get; set; }
    
    public DateTime Date { get; set; }

    public string Responsible { get; set; }
    
    public string Deadline { get; set; }
    
    public string Status { get; set; }
    
    public string Comments { get; set; }
    
    public bool Approval { get; set; }
    
}