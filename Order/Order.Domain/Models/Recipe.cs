using Shared;

namespace Order.Domain.Models
{
    public class Recipe : BaseModel<int>
    {
        public Recipe(int id, string name, Money price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public string Name { get; set; }
        public Money Price { get; set; }
    }
}