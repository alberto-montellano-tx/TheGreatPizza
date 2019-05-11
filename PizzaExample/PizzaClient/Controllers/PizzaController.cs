using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using PizzaClient.Models;
using PizzaClient.Repository;
using PizzaClient.ViewModel;

namespace PizzaClient.Controllers
{
    public class PizzaController : Controller
    {
        
        public ActionResult GetAllPizzas()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/pizza/getPizzas");
                response.EnsureSuccessStatusCode();
                JArray pizzas = JArray.Parse(response.Content.ReadAsStringAsync().Result);

                List<PizzaViewModel> pizzasModel = new List<PizzaViewModel>();
                foreach (JObject pizza in pizzas)
                {
                    PizzaViewModel result = new PizzaViewModel();
                    result.Pizza = new Pizza();
                    result.Pizza.Id = pizza["Id"].Value<int>();
                    result.Pizza.Name = pizza["Name"].Value<string>();
                    result.Pizza.Ingredients = new List<Ingredient>();
                    foreach (JObject ingre in (JArray)pizza["Ingredients"])
                    {
                        result.Pizza.Ingredients.Add(new Ingredient { Id = ingre["Id"].Value<int>(), Name = ingre["Name"].Value<string>() });
                    }

                    pizzasModel.Add(result);
                }
                
                ViewBag.Title = "All Pizzas";
                return View(pizzasModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult EditPizza(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/pizza/getPizza/" + id);
            response.EnsureSuccessStatusCode();
            JObject pizza = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            
            PizzaViewModel result = new PizzaViewModel();
            result.Pizza = new Pizza();
            result.Pizza.Id = pizza["Id"].Value<int>();
            result.Pizza.Name = pizza["Name"].Value<string>();
            result.Pizza.Ingredients = new List<Ingredient>();
            foreach (JObject ingre in (JArray)pizza["Ingredients"])
            {
                result.Pizza.Ingredients.Add(new Ingredient { Id = ingre["Id"].Value<int>(), Name = ingre["Name"].Value<string>() });
            }

            IngredientController ingredientController = new IngredientController();
            var allIngredients = ingredientController.GetAll();
            result.AllIngredients = allIngredients.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString()
            });

            ViewBag.Title = "Edit Pizza";
            return View(result);
        }

        [HttpPost]
        public ActionResult Update(FormCollection fc)
        {
            ServiceRepository serviceObj = new ServiceRepository();

            JObject updatedPizza = new JObject();
            updatedPizza["Id"] = Int32.Parse(fc["Pizza.Id"]);
            updatedPizza["Name"] = fc["Pizza.Name"];
            var selectedIngredients = fc["SelectedIngredients"].Split(',');
            JArray selecteds = new JArray();
            foreach (string id in selectedIngredients)
            {
                JObject ingre = new JObject();
                ingre["Id"] = id;
                selecteds.Add(ingre);
            }
            updatedPizza["Ingredients"] = selecteds;


            HttpResponseMessage response = serviceObj.PutResponse("api/pizza/updatePizza", updatedPizza);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllPizzas");
        }

        public ActionResult Details(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/pizza/getPizza/" + id);
            response.EnsureSuccessStatusCode();
            JObject pizza = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            
            PizzaViewModel result = new PizzaViewModel();
            result.Pizza = new Pizza();
            result.Pizza.Id = pizza["Id"].Value<int>();
            result.Pizza.Name = pizza["Name"].Value<string>();
            result.Pizza.Ingredients = new List<Ingredient>();
            foreach (JObject ingre in (JArray)pizza["Ingredients"])
            {
                result.Pizza.Ingredients.Add(new Ingredient { Id = ingre["Id"].Value<int>(), Name = ingre["Name"].Value<string>() });
            }

            ViewBag.Title = "Pizza Details";
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            IngredientController ingredientController = new IngredientController();
            var allIngredients = ingredientController.GetAll();

            ViewBag.AllIngredients = allIngredients.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString()
            });

            PizzaViewModel pizzaViewModel = new PizzaViewModel();
            pizzaViewModel.Pizza = new Pizza();
            pizzaViewModel.Pizza.Ingredients = new List<Ingredient>();
            pizzaViewModel.Pizza.Name = "Sample";
            return View(pizzaViewModel);
        }

        [HttpPost]
        public ActionResult Create(FormCollection fc)
        {
            ServiceRepository serviceObj = new ServiceRepository();

            JObject newPizza = new JObject();
            
            newPizza["Name"] = fc["Pizza.Name"];
            var selectedIngredients = fc["SelectedIngredients"].Split(',');
            JArray selecteds = new JArray();
            foreach (string id in selectedIngredients)
            {
                JObject ingre = new JObject();
                ingre["Id"] = id;
                selecteds.Add(ingre);
            }
            newPizza["Ingredients"] = selecteds;
            HttpResponseMessage response = serviceObj.PostResponse("api/pizza/addPizza", newPizza);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllPizzas");
        }

        public ActionResult Delete(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.DeleteResponse("api/pizza/deletePizza?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllPizzas");
        }
    }
}