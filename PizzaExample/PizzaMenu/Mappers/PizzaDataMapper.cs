using Newtonsoft.Json.Linq;
using PizzaMenu.Models;

namespace PizzaMenu.Mappers
{
    public class PizzaDataMapper
    {
        private const string ID = "Id";
        private const string NAME = "Name";

        public JObject GetPizza(Pizza pizza)
        {
            var result = new JObject();
            result[ID] = pizza.Id;
            result[NAME] = pizza.Name;

            JArray ingredients = new JArray();
            foreach (Ingredient ing in pizza.Ingredients)
            {
                JObject ingred = new JObject();
                ingred["Id"] = ing.Id;
                ingred["Name"] = ing.Name;
                ingredients.Add(ingred);
            }
            result["Ingredients"] = ingredients;

            return result;
        }

        public JObject GetIngredient(Ingredient ingredient)
        {
            var result = new JObject();
            result[ID] = ingredient.Id;
            result[NAME] = ingredient.Name;
            return result;
        }

    }
}