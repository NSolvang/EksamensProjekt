using Core;
using Core.Filter;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace ServerAPI.Controllers;

/// <summary>
/// API-controller til håndtering af brugere. Understøtter CRUD-operationer, login og filtreret søgning.
/// </summary>
[ApiController]
[Route("api/User")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository; // Repository til håndtering af brugerdata

    /// <summary>
    /// Konstruktør for UserController. Modtager repository via dependency injection.
    /// </summary>
    /// <param name="userRepository">Repository til brugerdata.</param>
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Henter alle brugere.
    /// </summary>
    /// <returns>En liste af alle brugere.</returns>
    [HttpGet]
    public async Task<IEnumerable<User>> Get()
    {
        var users = await _userRepository.GetAll();
        return users;
    }

    /// <summary>
    /// Henter en bruger baseret på ID.
    /// </summary>
    /// <param name="id">ID for brugeren der ønskes hentet.</param>
    /// <returns>Brugerobjekt hvis fundet, ellers 404.</returns>
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

    /// <summary>
    /// Tilføjer en ny bruger.
    /// </summary>
    /// <param name="user">Brugerobjekt der skal tilføjes.</param>
    /// <returns>201 Created hvis succesfuldt, ellers 400 ved valideringsfejl.</returns>
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

    /// <summary>
    /// Sletter en bruger baseret på ID.
    /// </summary>
    /// <param name="id">ID for brugeren der ønskes slettet.</param>
    [HttpDelete("{id}")]
    public void DeleteById(int id)
    {
        Console.WriteLine($"Sletter bruger med id {id}");
        _userRepository.DeleteById(id);
    }

    /// <summary>
    /// Opdaterer en eksisterende bruger.
    /// </summary>
    /// <param name="user">Det opdaterede brugerobjekt.</param>
    /// <returns>200 OK ved succes, 500 ved fejl.</returns>
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

    /// <summary>
    /// Logger en bruger ind via brugernavn og kodeord.
    /// </summary>
    /// <param name="loginDto">Objekt med brugernavn og adgangskode.</param>
    /// <returns>Brugerobjekt ved succes, 401 Unauthorized ved fejl.</returns>
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

    /// <summary>
    /// Henter brugere baseret på filtreringsparametre.
    /// </summary>
    /// <param name="filter">Filterparametre (f.eks. navn, rolle mv.).</param>
    /// <returns>En filtreret liste af brugere.</returns>
    [HttpGet("filtered")]
    public async Task<IActionResult> GetFilteredUsers([FromQuery] UserFilter filter)
    {
        var users = await _userRepository.GetFilteredUsers(filter);
        return Ok(users);
    }
}
