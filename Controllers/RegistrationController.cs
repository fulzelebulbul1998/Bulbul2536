using COOKING_RECIPE_PORTAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COOKING_RECIPE_PORTAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegistrationController : ControllerBase
    {
        private readonly LoginContext _dbContext;
        public RegistrationController(LoginContext dbContext)
        {
            _dbContext = dbContext;
        }
                    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetRegisterUser()
        {
            if (_dbContext.Login == null)
            {
                return NotFound();
            }

            return await _dbContext.Login.ToListAsync();
        }
  
        [HttpGet("{id}")]
        public async Task<ActionResult<Login>> GetUserById(int id)
        {
            if (_dbContext.Login == null)
            {
                return NotFound();
            }

            var login = await _dbContext.Login.FindAsync(id);
            return login;
        }

        [HttpPost]
        public async Task<ActionResult<Login>> PostUser(Login login)
        {
            _dbContext.Login.Add(login);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = login.Id }, login);
        }

         [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_dbContext.Login == null)
            {
                return NotFound();
            }

            var login = await _dbContext.Login.FindAsync(id);

            if (login == null)
            {
                return NotFound();
            }

            _dbContext.Remove(login);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
