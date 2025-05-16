using Core;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;


namespace ServerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task <IEnumerable<User>> Get()
    {
        var users = await _userRepository.GetAll();
        return users;
    }

    [HttpPost]
    public void Add(User user)
    {
        
        _userRepository.AddUser(user);
        
    }
    
    [HttpDelete("{id:int}")]
    public void DeleteById(int id)
    {
        Console.WriteLine($"Sletter annonce med id {id}");
        _userRepository.DeleteById(id);
    }

    public async Task UpdateUser(User user)
    {
        await _userRepository.UpdateUser(user);
    }
    
    
}