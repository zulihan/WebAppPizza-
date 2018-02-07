using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using adm = WebAppPizza.Areas.Admin.Models;

namespace WebAppPizza.Models
{
    public class CartLine
    {
        public int IDCartLine { get; set; }

        public adm.PizzaViewModel PizzaVM { get; set; }

        public int Quantity { get; set; }
    }
}
