using Microsoft.AspNetCore.Mvc;
using RestaurantRaterApi2.Data;
using Microsoft.EntityFrameworkCore;

namespace RestaurantRaterApi2.Controllers;

[ApiController]
[Route("[controller]")]

public class RestaurantController : ControllerBase
{
    private readonly RestaurantDbContext _context;
    public RestaurantController(RestaurantDbContext context)
    {
        _context = context;
    }

    //Get endpoints here...
    public async Task<IActionResult> GetRestaurants()
    {
        List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
        return Ok(restaurants);
    }

    [HttpGet("{id:int}")]

    public async Task<IActionResult> GetRestaurantById (int id)
    {
        Restaurant? restaurant = await _context.Restaurants.FindAsync(id);
        
        if (restaurant is null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }
    // Post endpoints here...

    [HttpPost]
    public async Task<IActionResult> PostRestaurant([FromForm] Restaurant request) 
    {

        if (ModelState.IsValid)
        {
            _context.Restaurants.Add(request);
            await _context.SaveChangesAsync();
            return Ok();
        }
        return BadRequest(ModelState);
    }
}
