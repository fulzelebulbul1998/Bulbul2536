using COOKING_RECIPE_PORTAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COOKING_RECIPE_PORTAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginContext _dbContext;
        private readonly IJWTManagerRepository _repository;

        public LoginController(LoginContext dbContext, IJWTManagerRepository repository)
        {
            _dbContext = dbContext;   
            _repository = repository;   
        }

        [HttpGet]
        [Route("{email}/{password}")]
        public IActionResult ValidateUser(string email, string password)
        {
            var jwtToken = _repository.Authenticate(email, password);
            if (jwtToken != null)
            {
                return Ok(jwtToken);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
