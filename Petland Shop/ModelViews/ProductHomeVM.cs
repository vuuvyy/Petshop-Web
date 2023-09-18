using Petland_Shop.Models;

namespace Petland_Shop.ModelViews
{
    public class ProductHomeVM
    {
        public Category category { get; set; }
        public List<Product> lsProducts { get; set; }

        public List<Product> lsBestSeller { get; set; }
    }
}
