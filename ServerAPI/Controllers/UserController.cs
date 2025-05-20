using Core;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;
using LoginRequest = Core.LoginRequest;


namespace ServerAPI.Controllers;

[ApiController]
[Route("api/User")]
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
    
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)  // eller int id hvis du bruger det som int
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _userRepository.AddUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
    }


    
    [HttpDelete("{id}")]
    public void DeleteById(int id)
    {
        Console.WriteLine($"Sletter annonce med id {id}");
        _userRepository.DeleteById(id);
    }

    [HttpPut]
    public async Task UpdateUser(User user)
    {
        await _userRepository.UpdateUser(user);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<User>> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _userRepository.Login(loginRequest.Username, loginRequest.Password);

        if (user == null)
        {
            return Unauthorized("Forkert brugernavn eller adgangskode.");
        }

        return Ok(user);
    }

    
    
}