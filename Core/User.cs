using System.ComponentModel.DataAnnotations;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Core;

public class User
{
    public int _id { get; set; }
    
    [Required(ErrorMessage = "Rolle er påkrævet")]
    public string Role { get; set; }
    
    [Required(ErrorMessage = "Brugernavn er påkrævet")]
    
    public bool IsActive { get; set; } =  true;
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "Adgangskode er påkrævet")]
    /*[StringLength(10, MinimumLength = 8, ErrorMessage = "Adgangskoden skal være mellem 8 og 10 tegn")]*/
    public string Password { get; set; }
    public string ProfilePicture { get; set; }
    
    [Required(ErrorMessage = "Lokation er påkrævet")]
    public Location? Location { get; set; }
    public string? Education { get; set; }
    
    [Required(ErrorMessage = "Navn er påkrævet")]
    public string Name { get; set; }
    
    public string? Internshipyear { get; set; } 
    
    public Studentplan? Studentplan { get; set; }

    //Dato for hvornår eleven starter
    public DateTime DateOfStart { get; set; } = DateTime.Today;

    public DateTime DateOfEnd { get; set; } = DateTime.Today.AddYears(3).AddMonths(3);

}
