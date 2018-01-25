using System;
using System.Text;

namespace WebAppPizza.Models
{
    public class Pizza
    {
        public int IDPizza { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal PriceHT { get; set; }

        public string Image { get; set; }

        public DateTime? Date { get; set; }   

        public Pizza()
        {
            Date = null;           

        }

        public Pizza(int idPizza, string title, decimal priceHT)
        {
            this.IDPizza = idPizza;
            this.Title = title;
            this.PriceHT = priceHT;
        }

        public Pizza(int idPizza, string title, decimal priceHT, string description, string image)
            : this(idPizza, title, priceHT)
        {            
            this.Description = description;
            this.Image = image;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var pi in typeof(Pizza).GetProperties())
            {
                sb.AppendFormat("{0} => {1}", pi.Name, pi.GetValue(this));
            }

            return sb.ToString();
        }
    }
}