using Core;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace ServerAPI.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationController : ControllerBase
{
    private readonly ILocationRepository _locationRepository;

    public LocationController(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    // GET: api/locations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Location>>> GetAllLocations()
    {
        try
        {
            var locations = await _locationRepository.GetAllLocations();
            return Ok(locations);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/locations/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Location>> GetLocationById(int id)
    {
        try
        {
            var location = await _locationRepository.GetLocationById(id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // POST: api/locations
    [HttpPost]
    public async Task<IActionResult> AddLocation([FromBody] Location location)
    {
        try
        {
            // Valider input
            if (location == null || string.IsNullOrWhiteSpace(location.Name))
            {
                return BadRequest("Location name is required");
            }

            var createdLocation = await _locationRepository.AddLocation(location);
            return CreatedAtAction(nameof(GetLocationById), new { id = createdLocation._id }, createdLocation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}