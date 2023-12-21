using COOKING_RECIPE_PORTAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COOKING_RECIPE_PORTAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly LoginContext _dbContext;
        public UserProfileController(LoginContext dbContext)
        {
            _dbContext = dbContext;
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Login>> GetUserById(int id)
        // {
        //     if (_dbContext.Login == null)
        //     {
        //         return NotFound();
        //     }

        //     var login = await _dbContext.Login.FindAsync(id);
        //     return login;
        // }

        [HttpGet("{id}")]
public async Task<ActionResult<Login>> GetUserById(int id)
{
    try
    {
        var login = await _dbContext.Login.FindAsync(id);

        if (login == null)
        {
            return NotFound(); // User with the specified ID was not found
        }

        return login;
    }
    catch (Exception ex)
    {
        // Log the exception or handle it in a way that is appropriate for your application
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}


        // [HttpPut]
        // public async Task<IActionResult> UpdateProfile(Login login)
        // {
        //     _dbContext.Entry(login).State = EntityState.Modified;

        //     try
        //     {
        //         await _dbContext.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ProfileAvailable(login.Id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //     return Ok();
        // }


    [HttpPut]
public async Task<IActionResult> UpdateProfile(Login login)
{
    try
    {
        _dbContext.Entry(login).State = EntityState.Modified;

        await _dbContext.SaveChangesAsync();

        return Ok(); // Return 200 OK if the update is successful
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!ProfileAvailable(login.Id))
        {
            return NotFound(); // Return 404 Not Found if the profile doesn't exist
        }
        else
        {
            throw; // Rethrow the exception if it's not a concurrency issue
        }
    }
    catch (Exception ex)
    {
        // Log the exception or handle it in a way that is appropriate for your application
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}

        private bool ProfileAvailable(int id)
        {
            return (_dbContext.Login?.Any(x => x.Id == id)).GetValueOrDefault();
        }
    }
}
