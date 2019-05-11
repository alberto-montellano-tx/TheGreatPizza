using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using PizzaClient.Models;
using PizzaClient.Repository;

namespace PizzaClient.Controllers
{
    public class IngredientController : Controller
    {
        private const string GETALLINGREDIENTS = "GetAllIngredients";

        public List<Ingredient> GetAll()
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/ingredient/getIngredients");
            response.EnsureSuccessStatusCode();
            JArray ingredients = JArray.Parse(response.Content.ReadAsStringAsync().Result);

            List<Ingredient> ingredientsModel = new List<Ingredient>();
            foreach (JObject ingredient in ingredients)
            {
                Ingredient result = new Ingredient();
                result.Id = ingredient["Id"].Value<int>();
                result.Name = ingredient["Name"].Value<string>();

                ingredientsModel.Add(result);

            }

          
            return ingredientsModel;
        }

        public ActionResult GetAllIngredients()
        {
            try
            {
                
                ViewBag.Title = "All Ingredients";
                return View(GetAll());
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]  
        public ActionResult EditIngredient(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/ingredient/GetIngredient/" + id.ToString());
            response.EnsureSuccessStatusCode();
            JObject ingredient = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            Ingredient result = new Ingredient();
            result.Id = ingredient["Id"].Value<int>();
            result.Name = ingredient["Name"].Value<string>();

            ViewBag.Title = "Edit Ingredient";
            return View(result);
        }

        [HttpPost]  
        public ActionResult Update(Ingredient ingredient)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PutResponse("api/ingredient/UpdateIngredient", ingredient);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(GETALLINGREDIENTS);
        }

        public ActionResult Details(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/ingredient/getIngredient/" + id);
            response.EnsureSuccessStatusCode();
            JObject ingredient = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            Ingredient result = new Ingredient();
            result.Id = ingredient["Id"].Value<int>();
            result.Name = ingredient["Name"].Value<string>();

            ViewBag.Title = "Ingredient Details";
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Ingredient ingredient)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PostResponse("api/ingredient/addIngredient", ingredient);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(GETALLINGREDIENTS);
        }

        public ActionResult Delete(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.DeleteResponse("api/ingredient/DeleteIngredient?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction(GETALLINGREDIENTS);
        }
    }
}