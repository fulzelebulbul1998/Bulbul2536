using System.ComponentModel.DataAnnotations;

namespace COOKING_RECIPE_PORTAL.Model
{
    public class Veg_Recipe
    {
        [Key]
        public int Recipe_Id { get; set; }
        public string Recipe_Name { get; set; }
       public string ingredient { get; set; }
          public string procedure { get; set; }
          public string recipeimages{get;set;}
    }
}
