using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using adm = WebAppPizza.Areas.Admin.Models;

namespace WebAppPizza.Models
{
    public class Cart
    {
        private List<CartLine> lines = new List<CartLine>();

        public virtual void AddItem(adm.PizzaViewModel pizzaVM, int quantity = 1)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            CartLine line = lines.Where(l => l.PizzaVM.IDPizza == pizzaVM.IDPizza).FirstOrDefault(); // = new CartLine();
            //CartLine line2 = lines.FirstOrDefault(l => l.PizzaVM.IDPizza == pizzaVM.IDPizza);
            //CartLine line3 = lines.Find(l => l.PizzaVM.IDPizza == pizzaVM.IDPizza);
            //CartLine line4 = lines.Single(l => l.PizzaVM.IDPizza == pizzaVM.IDPizza);
            sw.Stop();
            Debug.WriteLine(sw.ElapsedMilliseconds);

            if (line == null)
            {
                lines.Add(new CartLine { PizzaVM = pizzaVM, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(adm.PizzaViewModel pizzaVM)
        {
            lines.RemoveAll(l => l.PizzaVM.IDPizza == pizzaVM.IDPizza);
        }

        public decimal ComputeToTal() => lines.Sum(l => l.PizzaVM.PriceTTC * l.Quantity);

        public void Clear() => lines.Clear();
    }
}