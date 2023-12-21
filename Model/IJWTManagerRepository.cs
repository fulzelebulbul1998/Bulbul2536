using COOKING_RECIPE_PORTAL.Data;

namespace COOKING_RECIPE_PORTAL.Model
{
    public interface IJWTManagerRepository
    {
        MyJwtToken Authenticate(string? email, string? password);
    }
}
