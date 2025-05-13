using System.ComponentModel.DataAnnotations;
using System;
namespace Core;

public class User
{
    
    public int UserId { get; set; }
    
    [Required(ErrorMessage = "Rolle er påkrævet")]
    public string Role { get; set; }
    
    [Required(ErrorMessage = "Brugernavn er påkrævet")]
    public string UserName { get; set; }
    
    public string ProfilePicture { get; set; }
    
    [Required(ErrorMessage = "Adgangskode er påkrævet")]
   /*[StringLength(10, MinimumLength = 8, ErrorMessage = "Adgangskoden skal være mellem 8 og 10 tegn")]*/
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Lokation er påkrævet")]
    public string Location { get; set; }
    
    [Required(ErrorMessage = "Navn er påkrævet")]
    public string Name { get; set; }
    
}
