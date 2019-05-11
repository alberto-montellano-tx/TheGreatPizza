
using System.Collections.Generic;

namespace PizzaClient.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}