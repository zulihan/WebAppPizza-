using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppPizza.Models
{
    public class PizzaViewModel
    {
        public Pizza Pizza { get; set; }


        public PizzaViewModel()
        {

        }

        public decimal PriceTTC()
        {
            return this.Pizza.PriceHT * 1.2m;
        }
             

        // Mon code tout pourri

        //public int NumOfPages()        {
        //    int TotalPizzas = Pizza.Count;
        //    var Pages = 0;

        //    if((TotalPizzas % 5 != 0) && (TotalPizzas % 5 < 5))
        //    {
        //        Pages = Math.Floor(TotalPizzas / 5) + 1;
        //    }
        //    else
        //    {
        //        Pages = TotalPizzas / 5;
        //    }

        //    return Pages;            
        //}
                

    }
}
