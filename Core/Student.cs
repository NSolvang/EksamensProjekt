namespace Core;

public class Student  : User
{
    public Studentplan Studentplan { get; set; }
    
    //Dato for hvornår eleven starter
    public DateTime DateOfStart { get; set; } = DateTime.Today;
}