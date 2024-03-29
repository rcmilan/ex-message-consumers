﻿using Shared;

namespace Order.Domain.Models
{
    public class Order : BaseModel<Guid>, IBaseEvent
    {
        public static Order CreateNew()
        {
            return new Order();
        }

        private Order()
        {
            Id = Guid.NewGuid();
        }

        public IList<Recipe> Recipes { get; private set; } = new List<Recipe>();

        public decimal Total { get => Recipes.Sum(r => r.Price.Amount); }

        public string Description => $"[{Id}] valor: {Total}";

        public Order AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);

            return this;
        }

    }
}
