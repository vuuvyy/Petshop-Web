using Petland_Shop.Models;
using System.ComponentModel.DataAnnotations;

namespace Petland_Shop.ModelViews
{
    public class CartItem
    {
        
        public Product product { get; set; }
        public int amount { get; set; }
        public double TotalMoney => amount * product.Price.Value;
    }
}
