using System;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using PizzaMenu.Models;
using PizzaMenu.Mappers;

namespace IngredientMenu.Controllers
{
    [RoutePrefix("Api/Ingredient")]
    public class IngredientController : ApiController
    {
        PizzaDataMapper _ingredientDataMapper = new PizzaDataMapper();
        PizzaExampleEntities objEntity = new PizzaExampleEntities();

        [HttpGet]
        [Route("getIngredients")]
        public IHttpActionResult GetIngredients()
        {
            try
            {
                var ingredients = objEntity.Ingredients;
                JArray results = new JArray();
                foreach (Ingredient p in ingredients)
                {
                    JObject r = _ingredientDataMapper.GetIngredient(p);
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
        [Route("getIngredient/{ingredientId}")]
        public IHttpActionResult GetIngredientById(string ingredientId)
        {
            Ingredient objEmp = new Ingredient();
            int id = Convert.ToInt32(ingredientId);
            JObject ingredientFound = null;

            try
            {
                objEmp = objEntity.Ingredients.Find(id);
                if (objEmp == null)
                {
                    return NotFound();
                }

                ingredientFound = _ingredientDataMapper.GetIngredient(objEmp);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(ingredientFound);
        }

        [HttpPost]
        [Route("addIngredient")]
        public IHttpActionResult PostIngredient(Ingredient data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                objEntity.Ingredients.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }


            return Ok(data);
        }

        [HttpPut]
        [Route("updateIngredient")]
        public IHttpActionResult PutIngredient(Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Ingredient ingredientFromDb = new Ingredient();
                ingredientFromDb = objEntity.Ingredients.Find(ingredient.Id);
                if (ingredientFromDb != null)
                {
                    ingredientFromDb.Name = ingredient.Name;
                }
                int i = this.objEntity.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(ingredient);
        }

        [HttpDelete]
        [Route("deleteIngredient")]
        public IHttpActionResult DeleteIngredient(int id)
        {
            Ingredient ingredient = objEntity.Ingredients.Find(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            objEntity.Ingredients.Remove(ingredient);
            objEntity.SaveChanges();

            return Ok(ingredient);
        }
    }
}