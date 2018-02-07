using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAppPizza.Models;
using adm = WebAppPizza.Areas.Admin.Models;

namespace WebAppPizza.Controllers
{
    public class PizzaController : Controller
    {

        private IRepositoryable<Pizza> _pizzaRepository;
        private IConfiguration _configuration;
        private readonly int itemsPerPage;

        public List<Pizza> Pizzas { get; set; } = new List<Pizza>()
        {
            new Pizza(1, "Reine", 10.40m, "Sauce tomate, Jambon, champignons....", "Pizza_reine.jpg" ),
            new Pizza(2, "Saumon", 12.40m, "crème fraîche, Saumon, câpres....", "Pizza_saumon.jpg" ),
            new Pizza(3, "Chorizo", 11.40m, "Sauce tomate, Chorizo, champignons....", "Pizza_chorizo.jpg"),
            new Pizza(4, "Romaine", 11.40m, "Sauce tomate, Jambon, olives....", "Pizza_chorizo.jpg"),
            new Pizza(5, "Popeye", 11.40m, "Sauce tomate, Epinard, Saumon....", "Pizza_chorizo.jpg"),
            new Pizza(6, "Océane", 11.40m, "Sauce tomate, Mozzarella, Saint-Jacques....", "Pizza_chorizo.jpg"),
            new Pizza(7, "Chevrotine", 11.40m, "Sauce tomate, Chèvre, lardons....", "Pizza_chorizo.jpg"),
            new Pizza(8, "Tartiflette", 11.40m, "Sauce tomate, Pomme de terre, Lardons....", "Pizza_chorizo.jpg"),

        };


        public PizzaController(IRepositoryable<Pizza> pizzaRepository, IConfiguration configuration)

        {
            _configuration = configuration;
            _pizzaRepository = pizzaRepository;
            itemsPerPage = Convert.ToByte(_configuration.GetValue(typeof(byte), "itemsPerPage"));
        }

        [HttpGet("[controller]/[action]/Page/{page?}")]
        public IActionResult Index(int page = 1)
        {
            //    var pizzasVM = new List<PizzaViewModel>();


            //    ViewBag.ItemsCount = Math.Ceiling((decimal)Pizzas.Count / itemsPerPage);
            //    ViewBag.CurrentPage = page;

            //    Pizzas.Skip((page - 1) * 6).Take(6).ToList().ForEach(p =>
            //    {
            //        pizzasVM.Add(new PizzaViewModel() { Pizza = p });
            //    });


            // Pour chaque itération de pizza les pizzas du modèle devinnent des pizzas ViewModel
            // Du coup dans index on peut leur appliquer la méthode PriceTTC
            //Pizzas.Where(p => p.PriceHT < 12).ToList().ForEach(p =>
            //{
            //    pizzasVM.Add(new PizzaViewModel() { Pizza = p });
            //});

            /*var query2 = from p in Pizzas
                         where p.PriceHT < 12
                         select p;

            var champQuery = Pizzas.Where(p => p.Description.Contains("champignons"));
            var queryMax = Pizzas.Max(p => p.PriceHT);*/

            //return View(pizzasVM);
            var pizzasVM = new List<adm.PizzaViewModel>();            

            ViewBag.ItemsCount = Math.Ceiling((decimal)((PizzaRepository)this._pizzaRepository).Count() / itemsPerPage);
            ViewBag.CurrentPage = page;

            //foreach (var pizza in _pizzaRepository.Read((page - 1) * itemsPerPage, itemsPerPage))

                this._pizzaRepository.Read((page - 1) * itemsPerPage, itemsPerPage).ForEach(pizza =>
                {
                    pizzasVM.Add(new adm.PizzaViewModel()
                    {
                        Description = pizza.Description,
                        Image = pizza.Image,
                        PriceHT = pizza.PriceHT,
                        Title = pizza.Title,
                        IDPizza = pizza.IDPizza
                    });
                });

            return View(pizzasVM);
        }

        public PartialViewResult Detail(int id)
        {
            //var pizza = this.Pizzas.Find(p => p.IDPizza == id);

            //var pizzaVM = new PizzaViewModel()
            //{
            //    Pizza = pizza
            //};            
            var pizza = this._pizzaRepository.ReadById(id);

            var pizzaVM = new adm.PizzaViewModel()
            {
                Description = pizza.Description,
                Image = pizza.Image,
                PriceHT = pizza.PriceHT,
                Title = pizza.Title,
                IDPizza = pizza.IDPizza
        };
            return PartialView("DetailPartial", pizzaVM);
        }
    }
}