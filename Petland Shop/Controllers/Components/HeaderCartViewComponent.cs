using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petland_Shop.ModelViews;
using Petland_Shop.Extension;


namespace Petland_Shop.Controllers.Components
{
    public class HeaderCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            return View(cart);
        }
    }
}
