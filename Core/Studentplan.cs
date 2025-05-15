namespace Core;

public class Studentplan
{
    public int StudentplanID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public List<Goal> Goal { get; set; }
    
}