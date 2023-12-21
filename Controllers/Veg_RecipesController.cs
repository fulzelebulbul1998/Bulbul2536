using COOKING_RECIPE_PORTAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COOKING_RECIPE_PORTAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Veg_RecipesController : ControllerBase
    {
        private readonly LoginContext _dbContext;
        public Veg_RecipesController(LoginContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veg_Recipe>>> GetVegRecipes()
        {
            if (_dbContext.Veg_Recipe == null)
            {
                return NotFound();
            }

            return await _dbContext.Veg_Recipe.ToListAsync(); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Veg_Recipe>> GetVegRecipeById(int id)
        {
            if (_dbContext.Veg_Recipe == null)
            {
                return NotFound();
            }

            var veg_recipe = await _dbContext.Veg_Recipe.FindAsync(id);
            return veg_recipe;
        }

        [HttpPost]
        public async Task<ActionResult<Veg_Recipe>> GetVegRecipeById(Veg_Recipe veg_recipe)
        {          
            //Console.WriteLine(veg_recipe);

            // if (veg_recipe != null)
            // {
            //     _dbContext.Veg_Recipe.Add(veg_recipe);

            //     try
            //     {
            //         await _dbContext.SaveChangesAsync();
            //     }
            //     catch (Exception e)
            //     {
            //         Console.WriteLine(e.Message);

            //         return Ok(false);
            //     }
            // }
            // else
            // {
            //     return Ok("not");
            // }

            _dbContext.Veg_Recipe.Add(veg_recipe);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVegRecipeById), new { id = veg_recipe.Recipe_Id }, veg_recipe);
        }

        [HttpDelete("{id}")]              
        public async Task<IActionResult> DeleteVegRecipe(int id)
        {
            if (_dbContext.Veg_Recipe == null)
            {
                return NotFound();
            }

            var veg_recipe = await _dbContext.Veg_Recipe.FindAsync(id);

            if (veg_recipe == null)
            {
                return NotFound();
            }

            _dbContext.Remove(veg_recipe);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVeg(Veg_Recipe veg)
        {
            _dbContext.Entry(veg).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VegAvailable(veg.Recipe_Id))
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

        private bool VegAvailable(int id)
        {
            return (_dbContext.Veg_Recipe?.Any(x => x.Recipe_Id == id)).GetValueOrDefault();
        }
    }
}
