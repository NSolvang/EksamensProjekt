namespace Core;

public class Goal
{
    public int GoalId { get; set; }
    
    public string Name { get; set; }
    
    public List<Subgoal> Subgoals { get; set; }
}