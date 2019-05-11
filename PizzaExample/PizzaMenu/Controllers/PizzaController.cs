using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PizzaMenu.Mappers;
using PizzaMenu.Models;

namespace PizzaMenu.Controllers
{
    [RoutePrefix("Api/Pizza")]
    public class PizzasController : ApiController
    {
        PizzaDataMapper _pizzaDataMapper = new PizzaDataMapper();
        PizzaExampleEntities objEntity = new PizzaExampleEntities();

        [HttpGet]
        [Route("getPizzas")]
        public IHttpActionResult GetPizzas()
        {
            try
            {
                var pizzas = objEntity.Pizzas;
                JArray results = new JArray();
                foreach (Pizza p in pizzas)
                {
                    JObject r = _pizzaDataMapper.GetPizza(p);
                    results.Add(r);
                }

                return Ok(results);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getPizza/{pizzaId}")]
        public IHttpActionResult GetPizzaById(string pizzaId)
        {
            Pizza objPizza = new Pizza();
            int id = Convert.ToInt32(pizzaId);
            JObject pizzaFound = null;

            try
            {
                objPizza = objEntity.Pizzas.Find(id);
                if (objPizza == null)
                {
                    return NotFound();
                }

                pizzaFound = _pizzaDataMapper.GetPizza(objPizza);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(pizzaFound);
        }

        [HttpPost]
        [Route("addPizza")]
        public IHttpActionResult PostPizza(JObject data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                Pizza pizzaNew = new Pizza();
                
                if (pizzaNew != null)
                {
                    pizzaNew.Name = data["Name"].Value<string>();
                    List<Ingredient> newIngredients = new List<Ingredient>();

                    JArray ingredients = (JArray)data["Ingredients"];
                    foreach (var ing in ingredients)
                    {
                        newIngredients.Add(objEntity.Ingredients.Find(ing["Id"].Value<int>()));
                    }
                    
                    pizzaNew.Ingredients = newIngredients;
                }
                int i = this.objEntity.SaveChanges();


                objEntity.Pizzas.Add(pizzaNew);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }


            return Ok(data);
        }

        [HttpPut]
        [Route("updatePizza")]
        public IHttpActionResult PutPizza(JObject pizza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Pizza pizzaFromDb = new Pizza();
                pizzaFromDb = objEntity.Pizzas.Find(pizza["Id"].Value<int>());
                if (pizzaFromDb != null)
                {
                    pizzaFromDb.Name = pizza["Name"].Value<string>();
                    List<Ingredient> newIngredients = new List<Ingredient>();

                    JArray ingredients = (JArray)pizza["Ingredients"];
                    foreach (var ing in ingredients)
                    {
                        newIngredients.Add(objEntity.Ingredients.Find(ing["Id"].Value<int>()));
                    }

                    pizzaFromDb.Ingredients.Clear();
                    pizzaFromDb.Ingredients = newIngredients;
                }
                int i = this.objEntity.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(pizza);
        }

        [HttpDelete]
        [Route("deletePizza")]
        public IHttpActionResult DeletePizza(int id)
        {
            Pizza pizza = objEntity.Pizzas.Find(id);
            if (pizza == null)
            {
                return NotFound();
            }

            objEntity.Pizzas.Remove(pizza);
            objEntity.SaveChanges();

            return Ok(pizza);
        }
    }
}