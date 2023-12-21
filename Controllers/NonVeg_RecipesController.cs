using COOKING_RECIPE_PORTAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COOKING_RECIPE_PORTAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonVeg_RecipesController : ControllerBase
    {
        private readonly LoginContext _dbContext;
        public NonVeg_RecipesController(LoginContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NonVeg_Recipe>>> GetNonVegRecipes()
        {
            if (_dbContext.NonVeg_Recipe == null)
            {
                return NotFound();
            }

            return await _dbContext.NonVeg_Recipe.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NonVeg_Recipe>> GetNonVegRecipeById(int id)
        {
            if (_dbContext.NonVeg_Recipe == null)
            {
                return NotFound();
            }

            var nonveg_recipe = await _dbContext.NonVeg_Recipe.FindAsync(id);
            return nonveg_recipe;
        }

        [HttpPost]
        public async Task<ActionResult<NonVeg_Recipe>> GetNonVegRecipeById(NonVeg_Recipe nonveg_recipe)
        {
            _dbContext.NonVeg_Recipe.Add(nonveg_recipe);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNonVegRecipeById), new { id = nonveg_recipe.Recipe_Id }, nonveg_recipe);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNonVegRecipe(int id)
        {
            if (_dbContext.NonVeg_Recipe == null)
            {
                return NotFound();
            }

            var nonveg_recipe = await _dbContext.NonVeg_Recipe.FindAsync(id);

            if (nonveg_recipe == null)
            {
                return NotFound();
            }

            _dbContext.Remove(nonveg_recipe);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNonVeg(NonVeg_Recipe nonveg)
        {
            _dbContext.Entry(nonveg).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NonVegAvailable(nonveg.Recipe_Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool NonVegAvailable(int id)
        {
            return (_dbContext.NonVeg_Recipe?.Any(x => x.Recipe_Id == id)).GetValueOrDefault();
        }
    }
}
