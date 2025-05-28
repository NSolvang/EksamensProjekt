using Core;
using Core.Filter;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;


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
    public async Task<ActionResult<User>> GetUserById(int id) 
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
        return CreatedAtAction(nameof(GetUserById), new { id = user._id }, user);
    }
    
    [HttpDelete("{id}")]
    public void DeleteById(int id)
    {
        Console.WriteLine($"Sletter bruger med id {id}");
        _userRepository.DeleteById(id);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(User user)
    {
        try
        {
            await _userRepository.UpdateUser(user);
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine("FEJL: " + ex.Message);
            return StatusCode(500, ex.Message);
        }
    }


    
    [HttpPost("login")]
    public async Task<ActionResult<User>> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userRepository.Login(loginDto.Username, loginDto.Password);

        if (user == null)
        {
            return Unauthorized("Forkert brugernavn eller adgangskode.");
        }

        return Ok(user);
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> GetFilteredUsers([FromQuery] UserFilter filter)
    {
        var users = await _userRepository.GetFilteredUsers(filter);
        return Ok(users);
    }
    
    
}