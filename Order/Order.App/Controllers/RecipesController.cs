using Microsoft.AspNetCore.Mvc;
using Order.Domain.Models;

namespace Order.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IList<Recipe> _recipes = new List<Recipe>();

        public RecipesController()
        {
            _recipes.Add(new Recipe(1, "Prato A", new Shared.Money(100.11m, Shared.Currency.BRL)));
            _recipes.Add(new Recipe(2, "Prato B", new Shared.Money(950.22m, Shared.Currency.BRL)));
            _recipes.Add(new Recipe(3, "Prato C", new Shared.Money(110.33m, Shared.Currency.BRL)));

            _recipes.Add(new Recipe(4, "Food X", new Shared.Money(123.44m, Shared.Currency.USD)));
            _recipes.Add(new Recipe(5, "Food Y", new Shared.Money(234.55m, Shared.Currency.USD)));
            _recipes.Add(new Recipe(6, "Food Z", new Shared.Money(345.66m, Shared.Currency.USD)));

            _recipes.Add(new Recipe(7, "Option 1", new Shared.Money(111.77m, Shared.Currency.EUR)));
            _recipes.Add(new Recipe(8, "Option 2", new Shared.Money(222.88m, Shared.Currency.EUR)));
            _recipes.Add(new Recipe(9, "Option 3", new Shared.Money(333.99m, Shared.Currency.EUR)));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_recipes);
        }

        [HttpPost]
        public IActionResult Post()
        {
            var order = Domain.Models.Order
                .CreateNew()
                .AddRecipe(_recipes[8])
                .AddRecipe(_recipes[5])
                .AddRecipe(_recipes[1])
                .AddRecipe(_recipes[1])
                ;

            return Ok(order);
        }
    }
}
