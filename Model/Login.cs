using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;

namespace COOKING_RECIPE_PORTAL.Model
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter FirstName")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Enter LastName")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Enter Mobile_No")]
        public long Mobile_No { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Enter City")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Enter Age")]
        public int Age { get; set; }
    }
}
