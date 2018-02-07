using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebAppPizza.Extensions;
using Newtonsoft.Json;
using WebAppPizza.Areas.Admin.Models;

namespace WebAppPizza.Models
{
    public class SessionCart : Cart
    {
        [JsonIgnore]
        public ISession Session { get; set; }

        internal static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        public override void AddItem(Areas.Admin.Models.PizzaViewModel pizzaVM, int quantity = 1)
        {
            base.AddItem(pizzaVM, quantity);
            Session.SetJson("Cart", this);
        }
    }
}