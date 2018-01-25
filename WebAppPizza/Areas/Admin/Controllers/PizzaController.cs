using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using adm = WebAppPizza.Areas.Admin.Models;
using WebAppPizza.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using WebAppPizza.Services;

namespace WebAppPizza.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PizzaController : Controller
    {
        private IHostingEnvironment _env;
        private IStaticRepository _staticRepository { get; set; }

        public PizzaController(IHostingEnvironment env, IStaticRepository staticRepository)
        {
            _staticRepository = staticRepository;
            this._env = env;
        }

        public IActionResult Index()
        {
            var pizzasVM = new List<adm.PizzaViewModel>();

            foreach (var pizza in _staticRepository.Pizzas)
            {
                pizzasVM.Add(new adm.PizzaViewModel()
                {
                    Description = pizza.Description,
                    Image = pizza.Image,
                    PriceHT = pizza.PriceHT,
                    Title = pizza.Title,
                    IDPizza = pizza.IDPizza
                });
            }

            return View(pizzasVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(adm.PizzaViewModel pizzaVM)
        {
            long size = pizzaVM.UploadImage?.Length ?? 0;
            var filename = String.Empty;
            IActionResult returnPage = null;

            if(ModelState.IsValid)
            {
                if(size > 0)
                {
                    filename = await CreateFileOnServerAsync(pizzaVM.UploadImage);

                    var pizza = new Pizza()
                    {
                        Description = pizzaVM.Description,
                        Image = filename,
                        PriceHT = pizzaVM.PriceHT,
                        Title = pizzaVM.Title
                    };

                    //TODO:Save Pizza in db
                    _staticRepository.Pizzas.Add(pizza);
                }
            }
            else
            {
                ModelState.AddModelError("Error Model", "Les données saisies ne sont pas valides, veuillez vérifier celles-ci");
                returnPage =  View(pizzaVM);
            }

            if (pizzaVM.AddNewPizza)
                returnPage = RedirectToAction("Create");
            else
                returnPage = RedirectToAction("Index", "Pizza", new { area = "Admin" });


            return returnPage;
        }

        private async Task<string> CreateFileOnServerAsync(IFormFile uploadImage)
        {
            var webRoot = $@"{_env.WebRootPath}\images\";
            var filename = $"Pizza_{DateTime.Now.Ticks}.jpg";
            var filePath = Path.Combine(webRoot, filename);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                await uploadImage.CopyToAsync(fs);
            }

            return filename;

                throw new NotImplementedException();
        }
    }
}