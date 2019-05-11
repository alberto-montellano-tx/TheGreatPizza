using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PizzaClient.Models;

namespace PizzaClient.ViewModel
{
    public class PizzaViewModel
    {
        public PizzaViewModel()
        {
            AllIngredients = new List<SelectListItem>();
        }

        public Pizza Pizza { get; set; }
        public IEnumerable<SelectListItem> AllIngredients { get; set; }
        
        private int[] _selectedIngredients;
        public int[] SelectedIngredients
        {
            get
            {
                if (_selectedIngredients == null)
                {
                    _selectedIngredients = Pizza.Ingredients.Select(m => m.Id).ToArray();
                }
                return _selectedIngredients;
            }
            set { _selectedIngredients = value; }
        }
    }

}