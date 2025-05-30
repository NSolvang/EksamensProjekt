using Core;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace ServerAPI.Controllers
{
    /// <summary>
    /// Controller der håndterer API-endpoints til operationer på locations (praktiksteder).
    /// Indeholder endpoints til at hente, oprette og få en specifik location.
    /// </summary>
    [ApiController]
    [Route("api/locations")]
    public class LocationController : ControllerBase
    {
        // Repository til håndtering af dataadgang for locations
        private readonly ILocationRepository _locationRepository;

        /// <summary>
        /// Konstruktør der injicerer et ILocationRepository.
        /// </summary>
        /// <param name="locationRepository">Repository der bruges til at tilgå location-data.</param>
        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        /// <summary>
        /// Henter alle locations.
        /// </summary>
        /// <returns>En liste af alle location-objekter i systemet.</returns>
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

        /// <summary>
        /// Henter en specifik location baseret på ID.
        /// </summary>
        /// <param name="id">ID på den ønskede location.</param>
        /// <returns>Location-objektet hvis det findes, ellers NotFound.</returns>
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

        /// <summary>
        /// Opretter en ny location.
        /// </summary>
        /// <param name="location">Location-objekt der skal oprettes. Kræver at Name er udfyldt.</param>
        /// <returns>HTTP 201 Created med reference til den oprettede location, eller fejlstatus ved fejl.</returns>
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
}
